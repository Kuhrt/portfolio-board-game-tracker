using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BoardGameTracker.Common.Configuration;
using BoardGameTracker.Common.Exceptions;
using BoardGameTracker.Core.Models.Dtos.Users;
using BoardGameTracker.Core.Models.Requests.Auth;
using BoardGameTracker.Core.Models.Responses.Auth;
using BoardGameTracker.Core.Services.Interfaces;
using BoardGameTracker.Data.Models;
using BoardGameTracker.Data.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BoardGameTracker.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly JWTSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IMapper mapper,
        IOptions<JWTSettings> jwtSettings)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user == null)
        {
            throw new NotAuthorizedException("Invalid email or password");
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException("Your account has been deactivated");
        }

        var isPasswordValid = await _userRepository.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            throw new NotAuthorizedException("Invalid email or password");
        }

        var token = await GenerateJwtToken(user);
        var userDto = _mapper.Map<UserDto>(user);

        return new LoginResponse
        {
            Token = token.Token,
            ExpiresAt = token.ExpiresAt,
            User = userDto
        };
    }

    public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByEmail(request.Email);

        if (existingUser != null)
        {
            throw new ConflictException("A user with this email already exists");
        }

        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true,
            EmailConfirmed = true
        };

        user = await _userRepository.CreateUserAsync(user, request.Password);

        var token = await GenerateJwtToken(user);
        var userDto = _mapper.Map<UserDto>(user);

        return new LoginResponse
        {
            Token = token.Token,
            ExpiresAt = token.ExpiresAt,
            User = userDto
        };
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user == null)
        {
            // Don't reveal that the user doesn't exist for security reasons
            return "If an account with that email exists, a password reset link has been sent";
        }

        var token = await _userRepository.GeneratePasswordResetToken(user.Email!);

        // TODO: Send email with reset token
        // For now, return the token (in production, this should be sent via email)

        return token ?? string.Empty;
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userRepository.GetByEmail(request.Email);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        await _userRepository.ResetPassword(user, request.Token, request.NewPassword);
    }

    private async Task<(string Token, DateTime ExpiresAt)> GenerateJwtToken(ApplicationUser user)
    {
        var roles = await _userRepository.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.FullName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret ?? string.Empty));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }
}
