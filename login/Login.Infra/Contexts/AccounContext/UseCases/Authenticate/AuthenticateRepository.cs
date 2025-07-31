using Login.Core.Contexts.AccountContext.Entities;
using Login.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using Login.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Login.Infra.Contexts.AccounContext.UseCases.Authenticate;

public class AuthenticateRepository : IAuthenticateRepository
{
    private readonly AppDbContext _context;

    public AuthenticateRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken);
    }
}