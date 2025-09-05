using Dapper.Lib;
using Core;

namespace Tests.OrdersRepositoryTests
{
    [Collection("Database collection")]
    public class DappsOrdersRepositoryTests : OrdersRepositoryTests
    {
        public DappsOrdersRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        public override IOrdersRepository CreateRepositoryInstance()
        {
            return new OrdersRepository(this._fixture.ConnectionString);
        }
    }
}