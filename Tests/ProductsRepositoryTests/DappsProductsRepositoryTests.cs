using Dapper.Lib;
using Core;

namespace Tests.ProductsRepositoryTests
{
    [Collection("Database collection")]
    public class DappsProductsRepositoryTests : ProductsRepositoryTests
    {
        public DappsProductsRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        public override IProductsRepository CreateRepositoryInstance()
        {
            return new ProductsRepository(_fixture.ConnectionString);
        }
    }
}