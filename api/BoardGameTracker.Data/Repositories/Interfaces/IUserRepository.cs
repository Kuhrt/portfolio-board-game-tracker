using BoardGameTracker.Common.Models;
using BoardGameTracker.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BoardGameTracker.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<ApplicationUser>> GetAll(Guid currentUserGuid);
    Task<ApplicationUser?> Get(Guid userGuid);
    Task<ApplicationUser?> GetByEmail(string email);
    Task<(ApplicationUser user, string? resetPasswordToken)> AddOrUpdate(ApplicationUser user);
    Task Delete(Guid userGuid);
    Task ResetPassword(ApplicationUser user, string resetPasswordToken, string newPassword);
    Task<bool?> IsPasswordNull(string userName);
    Task<string?> GeneratePasswordResetToken(string userName);
    Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword);

}