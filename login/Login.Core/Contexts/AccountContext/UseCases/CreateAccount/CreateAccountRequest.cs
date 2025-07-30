using MediatR;

namespace Login.Core.Contexts.AccountContext.UseCases.CreateAccount
{
    public record CreateAccountRequest(string Name, string Email, string? Password) : IRequest<CreateAccountResponse>;
}
