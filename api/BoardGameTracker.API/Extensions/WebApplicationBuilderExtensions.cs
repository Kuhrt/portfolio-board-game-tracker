using Microsoft.EntityFrameworkCore;

namespace BoardGameTracker.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ConfigureDatabase<T>(this WebApplicationBuilder builder, string connectionName) where T : DbContext
    {
        builder.Services.AddDbContext<T>(
            options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(connectionName)
                )
        );
    }
}