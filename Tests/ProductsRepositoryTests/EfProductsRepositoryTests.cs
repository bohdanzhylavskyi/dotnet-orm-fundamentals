using Core;
using EF.Lib;

namespace Tests.ProductsRepositoryTests
{
    public class EFProductsRepositoryTests : ProductsRepositoryTests
    {
        public EFProductsRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
        }

        public override IProductsRepository CreateRepositoryInstance()
        {
            return new ProductsRepository(_fixture.AppDbContext);
        }
    }
}