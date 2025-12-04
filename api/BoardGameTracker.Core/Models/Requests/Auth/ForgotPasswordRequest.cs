using System.ComponentModel.DataAnnotations;

namespace BoardGameTracker.Core.Models.Requests.Auth;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
