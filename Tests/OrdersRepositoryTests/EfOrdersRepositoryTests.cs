using Core;
using EFLib;

namespace Tests.OrdersRepositoryTests
{
    [Collection("Database collection")]
    public class EfOrdersRepositoryTests : OrdersRepositoryTests
    {
        public EfOrdersRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        public override IOrdersRepository CreateRepositoryInstance()
        {
            return new OrdersRepository(this._fixture.AppDbContext);
        }
    }
}