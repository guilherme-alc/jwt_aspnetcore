using Login.Core.Contexts.AccountContext.Entities;
using Login.Infra.Contexts.AccounContext.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Login.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
