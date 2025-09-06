using Core;
using EF.Lib;

namespace Tests.OrdersRepositoryTests
{
    public class EFOrdersRepositoryTests : OrdersRepositoryTests
    {
        public EFOrdersRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        public override IOrdersRepository CreateRepositoryInstance()
        {
            return new OrdersRepository(this._fixture.AppDbContext);
        }
    }
}