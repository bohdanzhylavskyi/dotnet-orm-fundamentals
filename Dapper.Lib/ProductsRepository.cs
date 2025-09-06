using Core;
using Microsoft.Data.SqlClient;

namespace Dapper.Lib
{
    public class ProductsRepository : IProductsRepository
    {
        private string _connectionString;
        private readonly SqlDataAdapter _adapter;

        public ProductsRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void CreateProduct(Product product)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var id = connection.QuerySingle<int>(
                    "INSERT INTO Products (Name, Description, Weight, Height, Width, Length) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length);",
                    product
                );

                product.Id = id;
            }
        }

        public Product? GetProduct(int productId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.QueryFirstOrDefault<Product>(
                    "SELECT * FROM Products WHERE Id = @Id;",
                    new
                    {
                        Id = productId,
                    }
                );
            }
        }

        public void UpdateProduct(Product product)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(
                     "UPDATE Products SET Name=@Name, Description=@Description, Weight=@Weight, Height=@Height, Width=@Width, Length=@Length" +
                     " WHERE Id=@Id",
                    product
                );
            }
        }

        public void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(
                     "DELETE FROM Products WHERE Id = @Id;",
                    new { Id = productId }
                );
            }
        }

        public List<Product> ListProducts()
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.Query<Product>("SELECT * FROM Products;").ToList();
            }
        }
    }
}
