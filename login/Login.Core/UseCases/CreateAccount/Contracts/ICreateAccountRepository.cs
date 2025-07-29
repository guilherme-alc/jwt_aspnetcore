using Login.Core.Contexts.AccountContext.Entities;

namespace Login.Core.UseCases.CreateAccount.Contracts
{
    public interface ICreateAccountRepository
    {
        Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
        Task CreateAsync(User user, CancellationToken cancellationToken);
    }
}
