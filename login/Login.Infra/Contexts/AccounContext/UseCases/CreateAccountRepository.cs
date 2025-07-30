using Login.Core.Contexts.AccountContext.Entities;
using Login.Core.Contexts.AccountContext.UseCases.CreateAccount.Contracts;
using Login.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Login.Infra.Contexts.AccounContext.UseCases
{
    public class CreateAccountRepository : ICreateAccountRepository
    {
        private readonly AppDbContext _context;
        public CreateAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email.Address == email, cancellationToken);
        }

        public async Task CreateAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user);
            await SaveAsync(cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cacellationToken)
            => await _context.SaveChangesAsync(cacellationToken);
    }
}
