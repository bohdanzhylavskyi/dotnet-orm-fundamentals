using ADO.Lib;
using System.Transactions;

namespace ADO.Tests
{
    [Collection("Database collection")]
    public class ProductsRepositoryTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly ProductsRepository _repository;

        public ProductsRepositoryTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _repository = new ProductsRepository(this._fixture.ConnectionString);
        }


        [Fact]
        public void CreateProduct()
        {
            using (var scope = new TransactionScope())
            {
                var product = ProductStub();

                // act
                _repository.CreateProduct(product);

                // assert
                var retrievedProduct = _repository.GetProduct(product.Id);

                Assert.Equivalent(product, retrievedProduct);
            }
        }

        [Fact]
        public void GetProduct()
        {
            using (var scope = new TransactionScope())
            {
                var product = ProductStub();

                _repository.CreateProduct(product);

                // act
                var retrievedProduct = _repository.GetProduct(product.Id);

                Assert.Equivalent(product, retrievedProduct);
            }
        }

        [Fact]
        public void UpdateProduct()
        {
            using (var scope = new TransactionScope())
            {
                var product = ProductStub();

                _repository.CreateProduct(product);

                // act
                product.Name = "x";
                product.Description = "x";
                product.Height = 1.000m;
                product.Weight = 2.000m;
                product.Length = 3.000m;
                product.Width = 4.000m;

                _repository.UpdateProduct(product);

                // assert
                var retrievedProduct = _repository.GetProduct(product.Id);

                Assert.Equivalent(product, retrievedProduct);
            }
        }

        [Fact]
        public void DeleteProduct()
        {
            using (var scope = new TransactionScope())
            {
                var product = ProductStub();

                _repository.CreateProduct(product);

                // act
                _repository.DeleteProduct(product.Id);

                // assert
                var products = _repository.ListProducts();

                Assert.DoesNotContain(products, p => p.Id == product.Id);
            }
        }

        [Fact]
        public void ListProducts()
        {
            using (var scope = new TransactionScope())
            {
                var product1 = ProductStub();
                var product2 = ProductStub();

                _repository.CreateProduct(product1);
                _repository.CreateProduct(product2);

                // act
                var products = _repository.ListProducts();

                Assert.Contains(products, (p) => p.Id == product1.Id);
                Assert.Contains(products, (p) => p.Id == product2.Id);
            }
        }

        private Product ProductStub()
        {
            return new Product()
            {
                Name = "iPhone 15",
                Description = "Latest Apple smartphone",
                Weight = 0.174m,
                Height = 14.700m,
                Length = 7.100m,
                Width = 0.750m
            };
        }
    }
}