using Core;
using EFLib;

namespace Tests.ProductsRepositoryTests
{
    [Collection("Database collection")]
    public class EfProductsRepositoryTests : ProductsRepositoryTests
    {
        public EfProductsRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        public override IProductsRepository CreateRepositoryInstance()
        {
            return new ProductsRepository(_fixture.AppDbContext);
        }
    }
}