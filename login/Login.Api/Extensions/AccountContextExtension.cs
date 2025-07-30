using Login.Core.Contexts.AccountContext.UseCases.CreateAccount;
using Login.Core.Contexts.AccountContext.UseCases.CreateAccount.Contracts;
using Login.Infra.Contexts.AccounContext.UseCases;
using MediatR;

namespace Login.Api.Extensions
{
    public static class AccountContextExtension
    {
        public static void AddAccountContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<ICreateAccountRepository, CreateAccountRepository>();
            builder.Services.AddTransient<ICreateAccountService, CreateAccountService>();
        }

        public static void MapAccountContext(this WebApplication app)
        {
            app.MapPost("/api/account/create", async (CreateAccountRequest request,
                IRequestHandler<CreateAccountRequest, CreateAccountResponse> handler) =>
            {
                var result = await handler.Handle(request, new CancellationToken());
                return result.IsSuccess
                    ? Results.Created("", result)
                    : Results.BadRequest(result);
            });
        }
    }
}
