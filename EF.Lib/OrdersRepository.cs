using Core;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EF.Lib
{
    public class OrdersRepository : IOrdersRepository
    {
        private string _connectionString;
        private AppDbContext _dbContext;

        public OrdersRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreateOrder(Order order)
        {
            this._dbContext.Orders.Add(order);
            this._dbContext.SaveChanges();
        }


        public Order? GetOrder(int orderId)
        {
            return this._dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void UpdateOrder(Order order)
        {
            this._dbContext.Orders.Update(order);
            this._dbContext.SaveChanges();
        }

        public void DeleteOrder(int orderId)
        {
            this._dbContext.Orders.Where(o => o.Id == orderId).ExecuteDelete();
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
            return this._dbContext.Orders.ToList();
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
            var sql = "EXEC SearchOrders @Month = {0}, @Year = {1}, @Status = {2}, @ProductId = {3}";

            return _dbContext.Orders
                .FromSqlRaw(
                    sql,
                    filters.Month ?? (object)DBNull.Value,
                    filters.Year ?? (object)DBNull.Value,
                    filters.Status?.ToString() ?? (object)DBNull.Value,
                    filters.ProductId ?? (object)DBNull.Value
                )
                .ToList();
        }

        private void DeleteOrders(OrderFilters filters)
        {
            var sql = "EXEC DeleteOrders @Month = {0}, @Year = {1}, @Status = {2}, @ProductId = {3}";

            _dbContext.Database.ExecuteSqlRaw(
                sql,
                filters.Month,
                filters.Year,
                filters.Status?.ToString(),
                filters.ProductId
            );
        }
    }
}
