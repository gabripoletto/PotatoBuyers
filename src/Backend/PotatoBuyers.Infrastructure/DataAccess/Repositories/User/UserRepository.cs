using Microsoft.EntityFrameworkCore;
using PotatoBuyers.Domain.Entities.Users;
using PotatoBuyers.Domain.Repositories.User;

namespace PotatoBuyers.Infrastructure.DataAccess.Repositories.User
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly PotatoBuyersDbContext _dbContext;

        public UserRepository(PotatoBuyersDbContext dbContext) => _dbContext = dbContext;

        public async Task Insert(UserBase user) => await _dbContext.UserBases.AddAsync(user);
        public async Task<bool> ExistActiveUserWithEmail(string email) => await _dbContext.UserBases.AnyAsync(user => user.Email.Equals(email) && user.Active);

    }
}
