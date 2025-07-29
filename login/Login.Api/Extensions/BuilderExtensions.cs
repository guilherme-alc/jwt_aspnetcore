using Login.Core;
using Login.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Login.Api.Extensions
{
    public static class BuilderExtensions
    {
        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            Configuration.Database.ConnectionString = builder.Configuration
                .GetConnectionString("DefaultConnection") ?? string.Empty;

            Configuration.Secrets.PasswordSaltKey = builder.Configuration
                .GetSection("Secrets")
                .GetValue<string>("PasswordSaltKey") ?? string.Empty;
        }

        public static void AddDatabase(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.Database.ConnectionString, b =>
                {
                    b.MigrationsAssembly("Login.Api");
                });
            });
        }
    }
}
