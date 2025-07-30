using Login.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();

builder.AddAccountContext();

builder.AddMediator();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapAccountContext();

app.Run();