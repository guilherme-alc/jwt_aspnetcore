using Login.Core.Contexts.AccountContext.Entities;

namespace Login.Core.UseCases.CreateAccount.Contracts
{
    public interface ICreateAccountService
    {
        Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken);
    }
}
