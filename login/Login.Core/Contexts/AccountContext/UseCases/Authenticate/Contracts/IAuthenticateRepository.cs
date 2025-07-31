using Login.Core.Contexts.AccountContext.Entities;

namespace Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;

public interface IAuthenticateRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
}