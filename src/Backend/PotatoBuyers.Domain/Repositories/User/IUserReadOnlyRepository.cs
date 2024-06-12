namespace PotatoBuyers.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task ExistActiveUserWithEmail(string email);
    }
}
