using Core;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Dapper.Lib
{
    public class ProductsRepository : IProductsRepository
    {
        private string _connectionString;
        private readonly SqlDataAdapter _adapter;
        private readonly DataTable _productsTable;
        private bool _isInitialized = false;

        public ProductsRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this._adapter = new SqlDataAdapter("SELECT * FROM Products", _connectionString);
            this._productsTable = new DataTable();

            var insertCommand = new SqlCommand(
                "INSERT INTO Products (Name, Description, Weight, Height, Width, Length) " +
                "OUTPUT INSERTED.Id " +
                "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length);",
                new SqlConnection(_connectionString));

            insertCommand.Parameters.Add("@Name", SqlDbType.NVarChar, 0, "Name");
            insertCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 0, "Description");
            insertCommand.Parameters.Add("@Weight", SqlDbType.Decimal, 0, "Weight");
            insertCommand.Parameters.Add("@Height", SqlDbType.Decimal, 0, "Height");
            insertCommand.Parameters.Add("@Width", SqlDbType.Decimal, 0, "Width");
            insertCommand.Parameters.Add("@Length", SqlDbType.Decimal, 0, "Length");

            this._adapter.InsertCommand = insertCommand;
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
