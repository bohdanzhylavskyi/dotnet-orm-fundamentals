using Dapper.Lib;
using Core;

namespace Tests.ProductsRepositoryTests
{
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