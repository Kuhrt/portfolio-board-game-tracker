using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameTracker.Data.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    [PersonalData]
    public string? FirstName { get; set; }

    [PersonalData]
    public string? LastName { get; set; }
    public bool IsActive { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();
}