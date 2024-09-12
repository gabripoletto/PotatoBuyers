using PotatoBuyers.Domain.Entities.Users;

namespace PotatoBuyers.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<bool> ExistActiveUserWithEmail(string email);
        public Task<UserBase?> GetByEmailAndPassword(string email, string password);
    }
}
