using Moq;
using PotatoBuyers.Domain.Entities.Users;
using PotatoBuyers.Domain.Repositories.User;

namespace CommomTestUtilities.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private readonly Mock<IUserReadOnlyRepository> _repository;

        public UserReadOnlyRepositoryBuilder() => _repository = new Mock<IUserReadOnlyRepository>();

        public void ExistActiveUserWithEmail(string email)
        {
            _repository.Setup(repository => repository.ExistActiveUserWithEmail(email)).ReturnsAsync(true);
        }

        public void GetByEmailAndPassword(UserBase user)
        {
            _repository.Setup(repository => repository.GetByEmailAndPassword(user.Email, user.Password)).ReturnsAsync(user);
        }

        public IUserReadOnlyRepository Builder() => _repository.Object;
        
    }
}
