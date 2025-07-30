using Login.Core.Contexts.AccountContext.Entities;

namespace Login.Core.Contexts.AccountContext.UseCases.CreateAccount.Contracts
{
    public interface ICreateAccountRepository
    {
        Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
        Task CreateAsync(User user, CancellationToken cancellationToken);
        Task SaveAsync(CancellationToken cacellationToken);
    }
}
