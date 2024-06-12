using Microsoft.EntityFrameworkCore;
using PotatoBuyers.Domain.Entities.Users;

namespace PotatoBuyers.Infrastructure.DataAccess
{
    public class PotatoBuyersDbContext : DbContext
    {
        public PotatoBuyersDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserBase> UserBases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PotatoBuyersDbContext).Assembly);
        }
    }
}
