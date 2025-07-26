using JwtAspnet;
using JwtAspnet.Models;
using JwtAspnet.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.PrivateKey)),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", (TokenService service) => {
    var user = new User
    {
        Id = 1,
        Email = "guilherme@gmail.com",
        Password = "as3&258",
        Name = "Guilherme Campos",
        Image = "https://avatars.githubusercontent.com/u/99276750?v=4",
        Roles = new List<string> { "premium", "user" }
    };

    return service.Generate(user);
});

app.MapGet("/restrito", (TokenService service) => {
    return "Acesso permitido a rota restrita";
}).RequireAuthorization();

app.Run();
