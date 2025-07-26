using JwtAspnet;
using JwtAspnet.Extensions;
using JwtAspnet.Models;
using JwtAspnet.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
    {
        policy.RequireRole("admin");
    });

    options.AddPolicy("username", policy =>
    {
        policy.RequireUserName("guilherme@gmail.com");
    });

    options.AddPolicy("id", policy =>
    {
        policy.RequireClaim(ClaimTypes.NameIdentifier, ["1", "2"]);
    });
});

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

app.MapGet("/restrito", () => {
    return "Acesso permitido a rota de usuários autenticados";
}).RequireAuthorization();

app.MapGet("/admin", () => "Acesso de administrador liberado!")
    .RequireAuthorization("admin");

app.MapGet("/guilherme", () => "Acesso do usuário guilherme liberado!")
    .RequireAuthorization("username");

app.MapGet("/id", () => "Acesso de ids permitidos!")
    .RequireAuthorization("id");

app.MapGet("/claims", (ClaimsPrincipal claimsUser) => new { 
    id = claimsUser.Id(),
    name = claimsUser.Name(),
    email = claimsUser.Email(),
    givenName = claimsUser.GivenName(),
    image = claimsUser.Image(),
})
.RequireAuthorization();

app.Run();
