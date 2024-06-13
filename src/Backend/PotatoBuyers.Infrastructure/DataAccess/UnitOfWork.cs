using PotatoBuyers.Infrastructure.DataAccess.Repositories;

namespace PotatoBuyers.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PotatoBuyersDbContext _dbContext;

        public UnitOfWork(PotatoBuyersDbContext dbContext) => _dbContext = dbContext;

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
