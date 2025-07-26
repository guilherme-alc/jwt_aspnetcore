using JwtAspnet.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", (TokenService service) => {
    return service.Generate();
});

app.Run();
