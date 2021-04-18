using Bogus;
using Moq;
using Producer.Business.Repositories;
using Producer.Business.Services;
using Producer.Business.Tests.Fixtures;
using Producer.Shared.Data.Contexts;
using Producer.Shared.Holders;

namespace Producer.Business.Tests.Services
{
    public class SaleServiceTestBase
    {
        protected Faker Faker { get; } = Fixture.Get();
        protected Mock<ISaleRepository> Repository { get; }
        protected Mock<IUnitOfWork> UnitOfWork { get; }
        protected Mock<IMessageHolder> MessageHolder { get; }
        protected Mock<IEvent> Producer { get; }

        public SaleServiceTestBase()
        {
            Repository = new Mock<ISaleRepository>(MockBehavior.Strict);
            UnitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            MessageHolder = new Mock<IMessageHolder>(MockBehavior.Strict);
            Producer = new Mock<IEvent>(MockBehavior.Strict);
        }

        protected SalesService BuildSaleService() => new(
            Repository.Object,
            UnitOfWork.Object,
            MessageHolder.Object,
            Producer.Object);
    }
}
