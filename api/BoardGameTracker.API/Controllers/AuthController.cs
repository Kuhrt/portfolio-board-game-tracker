using BoardGameTracker.Core.Models.Api;
using BoardGameTracker.Core.Models.Requests.Auth;
using BoardGameTracker.Core.Models.Responses.Auth;
using BoardGameTracker.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoardGameTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        return Ok(new ApiResponse<LoginResponse> { Data = response });
    }

    /// <summary>
    /// Registers a new user and returns a JWT token
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _authService.RegisterAsync(request);
        return Ok(new ApiResponse<LoginResponse> { Data = response });
    }

    /// <summary>
    /// Initiates the password reset process by generating a reset token
    /// </summary>
    [HttpPost("forgot-password")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var message = await _authService.ForgotPasswordAsync(request);

        // Note: In production, the token should be sent via email
        // For development/testing purposes, we return it in the response
        return Ok(new ApiResponse<object>
        {
            Data = new
            {
                message,
                // TODO: Remove token from response in production
                token = message != "If an account with that email exists, a password reset link has been sent" ? message : null
            }
        });
    }

    /// <summary>
    /// Resets a user's password using a reset token
    /// </summary>
    [HttpPost("reset-password")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        await _authService.ResetPasswordAsync(request);
        return Ok(new ApiResponse<string> { Data = "Password has been reset successfully" });
    }
}
