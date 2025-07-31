using Login.Core.Contexts.AccountContext.UseCases.Authenticate;
using Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Login.Core.Contexts.AccountContext.UseCases.CreateAccount;
using Login.Core.Contexts.AccountContext.UseCases.CreateAccount.Contracts;
using Login.Infra.Contexts.AccounContext.UseCases.Authenticate;
using Login.Infra.Contexts.AccounContext.UseCases.CreateAccount;
using MediatR;

namespace Login.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            #region CreateAccount
            builder.Services.AddTransient<ICreateAccountRepository, CreateAccountRepository>();
            builder.Services.AddTransient<ICreateAccountService, CreateAccountService>();   
            #endregion

            #region Authenticate

            builder.Services.AddTransient<IAuthenticateRepository, AuthenticateRepository>();  

            #endregion
        }

        public static void MapAccountContext(this WebApplication app)
        {
            #region CreateAccount

            app.MapPost("/api/account/create", async (CreateAccountRequest request,
                IRequestHandler<CreateAccountRequest, CreateAccountResponse> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());
                return result.IsSuccess
                    ? Results.Created("", result)
                    : Results.BadRequest(result);
            });     

            #endregion

            #region Authenticate

            app.MapPost("/api/account/authenticate", async (AuthenticateRequest request,
                IRequestHandler<AuthenticateRequest, AuthenticateResponse> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());
                
                if(!result.IsSuccess)
                    return Results.Json(result, statusCode: result.Status);
                
                if(result.Data == null)
                    return Results.Json(result, statusCode: 500);
                
                result.Data.Token = JwtExtension.Generate(result.Data);
                
                return Results.Ok(result);
            });

            #endregion
        }
    }
}
