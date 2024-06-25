using PotatoBuyers.Domain.Entities.Users;

namespace PotatoBuyers.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task Insert(UserBase user);
    }
}
