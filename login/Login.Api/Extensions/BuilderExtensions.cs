using Login.Core;
using Login.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            Configuration.SendGrid.ApiKey = builder.Configuration
                .GetSection("Secrets")
                .GetValue<string>("ApiKey") ?? string.Empty;
            
            Configuration.Secrets.JwtPrivateKey = builder.Configuration
                .GetSection("Secrets")
                .GetValue<string>("JwtPrivateKey") ?? string.Empty;
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

        public static void AddJwtAuthentication(this WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.Secrets.PasswordSaltKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            builder.Services.AddAuthorization();
        }

        public static void AddMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(Configuration).Assembly);
            });
        }
    }
}