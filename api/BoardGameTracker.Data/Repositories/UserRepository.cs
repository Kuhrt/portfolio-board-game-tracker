using BoardGameTracker.Data.Models;
using BoardGameTracker.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BoardGameTracker.Data.Repositories;

public class UserRepository : IUserRepository
{
    public Task<(ApplicationUser user, string? resetPasswordToken)> AddOrUpdate(ApplicationUser user)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid userGuid)
    {
        throw new NotImplementedException();
    }

    public Task<string?> GeneratePasswordResetToken(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser?> Get(Guid userGuid)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationUser>> GetAll(Guid currentUserGuid)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationUser?> GetByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> IsPasswordNull(string userName)
    {
        throw new NotImplementedException();
    }

    public Task ResetPassword(ApplicationUser user, string resetPasswordToken, string newPassword)
    {
        throw new NotImplementedException();
    }
}