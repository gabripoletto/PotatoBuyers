using Moq;
using PotatoBuyers.Infrastructure.DataAccess.Repositories;

namespace CommomTestUtilities.Repositories
{
    public class UnitOfWorkBuilder
    {
        public static IUnitOfWork Build()
        {
            var mock = new Mock<IUnitOfWork>();

            return mock.Object;
        }
    }
}
