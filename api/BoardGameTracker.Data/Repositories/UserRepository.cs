using BoardGameTracker.Common.Exceptions;
using BoardGameTracker.Data.Models;
using BoardGameTracker.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BoardGameTracker.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public UserRepository(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<IEnumerable<ApplicationUser>> GetAll(Guid currentUserGuid)
    {
        return await _context.Users
            .Where(u => u.IsActive && u.Id != currentUserGuid)
            .OrderBy(u => u.LastName)
            .ThenBy(u => u.FirstName)
            .ToListAsync();
    }

    public async Task<ApplicationUser?> Get(Guid userGuid)
    {
        return await _context.Users.FindAsync(userGuid);
    }

    public async Task<ApplicationUser?> GetByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<(ApplicationUser user, string? resetPasswordToken)> AddOrUpdate(ApplicationUser user)
    {
        var existingUser = await _context.Users.FindAsync(user.Id);
        string? resetToken = null;

        if (existingUser == null)
        {
            await _context.Users.AddAsync(user);
            resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        else
        {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.UserName = user.Email;
            existingUser.IsActive = user.IsActive;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.EmailConfirmed = user.EmailConfirmed;
            existingUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;

            _context.Users.Update(existingUser);
        }

        await _context.SaveChangesAsync();
        return (existingUser ?? user, resetToken);
    }

    public async Task Delete(Guid userGuid)
    {
        var user = await _context.Users.FindAsync(userGuid);

        if (user == null)
        {
            throw new NotFoundException($"User with ID {userGuid} not found");
        }

        user.IsActive = false;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<string?> GeneratePasswordResetToken(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return null;
        }

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<bool?> IsPasswordNull(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return null;
        }

        return !await _userManager.HasPasswordAsync(user);
    }

    public async Task ResetPassword(ApplicationUser user, string resetPasswordToken, string newPassword)
    {
        var result = await _userManager.ResetPasswordAsync(user, resetPasswordToken, newPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Failed to reset password: {errors}");
        }
    }

    public async Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
    {
        return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Failed to create user: {errors}");
        }

        return user;
    }

    public async Task<ApplicationUser> UpdateUserAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequestException($"Failed to update user: {errors}");
        }

        return user;
    }

    public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}
