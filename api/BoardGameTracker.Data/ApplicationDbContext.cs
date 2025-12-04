using BoardGameTracker.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoardGameTracker.Data;

public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
    {
    }

    public virtual DbSet<ConfigurationSetting> ConfigurationSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>().ToTable("AspNetUsers", "Identity");
        builder.Entity<ApplicationRoleClaim>().ToTable("AspNetRoleClaims", "Identity");
        builder.Entity<ApplicationRole>().ToTable("AspNetRoles", "Identity");
        builder.Entity<ApplicationUserClaim>().ToTable("AspNetUserClaims", "Identity");
        builder.Entity<ApplicationUserLogin>().ToTable("AspNetUserLogins", "Identity");
        builder.Entity<ApplicationUserRole>().ToTable("AspNetUserRoles", "Identity");
        builder.Entity<ApplicationUserToken>().ToTable("AspNetUserTokens", "Identity");
    }

    partial void OnModelCreatingPartial(ModelBuilder builder);
}