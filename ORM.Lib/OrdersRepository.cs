using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.Lib
{
    public enum OrderStatus
    {
        NotStarted,
        Loading,
        InProgress,
        Arrived,
        Unloading,
        Cancelled,
        Done
    }

    public class Order
    {
        public int Id { get; set; }
        public required OrderStatus Status { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required DateTime UpdatedDate { get; set; }
        public required int ProductId { get; set; }
    }

    public struct OrderFilters
    {
        public int? Month { get; init; } = null;
        public int? Year { get; init; } = null;
        public OrderStatus? Status { get; init; } = null;
        public int? ProductId { get; init; } = null;
        public OrderFilters() { }
    }

    public class OrdersRepository
    {
        private string _connectionString;

        public OrdersRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public void CreateOrder(Order order)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var id = connection.QuerySingle<int>(
                    "INSERT INTO Orders (Status, CreatedDate, UpdatedDate, ProductId) " +
                    "OUTPUT INSERTED.Id " +
                    "VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId);",
                    this.ConvertOrderToDapperParams(order)
                );

                order.Id = id;
            }
        }


        public Order? GetOrder(int productId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.QueryFirstOrDefault<Order>(
                    "SELECT * FROM Orders WHERE Id = @Id;",
                    new
                    {
                        Id = productId,
                    }
                );
            }
        }

        public void UpdateOrder(Order order)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(
                     "UPDATE Orders SET Status=@Status, CreatedDate=@CreatedDate, UpdatedDate=@UpdatedDate, ProductId=@ProductId" +
                     " WHERE Id=@Id",
                    this.ConvertOrderToDapperParams(order)
                );
            }
        }

        public void DeleteOrder(int orderId)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Execute(
                     "DELETE FROM Orders WHERE Id = @Id;",
                    new { Id = orderId }
                );
            }
        }

        public List<Order> SearchOrdersByMonth(int month)
        {
            var filters = new OrderFilters()
            {
                Month = month
            };

            return SearchOrders(filters);
        }

        public List<Order> SearchOrdersByYear(int year)
        {
            var filters = new OrderFilters()
            {
                Year = year
            };

            return SearchOrders(filters);
        }

        public List<Order> SearchOrdersByStatus(OrderStatus status)
        {
            var filters = new OrderFilters()
            {
                Status = status
            };

            return SearchOrders(filters);
        }

        public List<Order> SearchOrdersByProduct(int productId)
        {
            var filters = new OrderFilters()
            {
                ProductId = productId
            };

            return SearchOrders(filters);
        }

        public List<Order> ListOrders()
        {
            using (SqlConnection connection = new(_connectionString))
            {
                return connection.Query<Order>("SELECT * FROM Orders;").ToList();
            }
        }

        public void DeleteOrdersByMonth(int month)
        {
            var filters = new OrderFilters()
            {
                Month = month
            };

            DeleteOrders(filters);
        }

        public void DeleteOrdersByYear(int year)
        {
            var filters = new OrderFilters()
            {
                Year = year
            };

            DeleteOrders(filters);
        }

        public void DeleteOrdersByStatus(OrderStatus status)
        {
            var filters = new OrderFilters()
            {
                Status = status
            };

            DeleteOrders(filters);
        }

        public void DeleteOrdersByProduct(int productId)
        {
            var filters = new OrderFilters()
            {
                ProductId = productId
            };

            DeleteOrders(filters);
        }

        private List<Order> SearchOrders(OrderFilters filters)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var parameters = new DynamicParameters();

                if (filters.Month is not null) parameters.Add("@Month", filters.Month);
                if (filters.Year is not null) parameters.Add("@Year", filters.Year);
                if (filters.Status is not null) parameters.Add("@Status", filters.Status.ToString());
                if (filters.ProductId is not null) parameters.Add("@ProductId", filters.ProductId);

                return connection.Query<Order>(
                    "SearchOrders",
                    parameters, commandType: CommandType.StoredProcedure
                ).ToList();
            }
        }

        private void DeleteOrders(OrderFilters filters)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                var parameters = new DynamicParameters();

                if (filters.Month is not null) parameters.Add("@Month", filters.Month);
                if (filters.Year is not null) parameters.Add("@Year", filters.Year);
                if (filters.Status is not null) parameters.Add("@Status", filters.Status.ToString());
                if (filters.ProductId is not null) parameters.Add("@ProductId", filters.ProductId);

                connection.Execute(
                    "DeleteOrders",
                    parameters, commandType: CommandType.StoredProcedure
                );
            }
        }

        private object ConvertOrderToDapperParams(Order order)
        {
            return new
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                ProductId = order.ProductId,
                Status = order.Status.ToString(),
            };
        }
    }
}
