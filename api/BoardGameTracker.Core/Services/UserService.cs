using AutoMapper;
using BoardGameTracker.Common.Exceptions;
using BoardGameTracker.Core.Models.Dtos.Users;
using BoardGameTracker.Core.Models.Requests.Auth;
using BoardGameTracker.Core.Services.Interfaces;
using BoardGameTracker.Data.Repositories.Interfaces;

namespace BoardGameTracker.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> GetCurrentUserAsync(Guid userId)
    {
        var user = await _userRepository.Get(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        if (!user.IsActive)
        {
            throw new ForbiddenException("Your account has been deactivated");
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateProfileAsync(Guid userId, UpdateProfileRequest request)
    {
        var user = await _userRepository.Get(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        // Check if email is changing and if it's already taken
        if (user.Email != request.Email)
        {
            var existingUser = await _userRepository.GetByEmail(request.Email);
            if (existingUser != null && existingUser.Id != userId)
            {
                throw new ConflictException("Email is already in use");
            }
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        user = await _userRepository.UpdateUserAsync(user);

        return _mapper.Map<UserDto>(user);
    }

    public async Task ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
    {
        var user = await _userRepository.Get(userId);

        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var result = await _userRepository.ChangePassword(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Failed to change password: {errors}");
        }
    }
}
