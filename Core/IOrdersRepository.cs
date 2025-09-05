namespace Core
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

    public interface IOrdersRepository
    {
        public void CreateOrder(Order order);
        public Order? GetOrder(int productId);
        public void UpdateOrder(Order order);
        public void DeleteOrder(int orderId);
        public List<Order> SearchOrdersByMonth(int month);
        public List<Order> SearchOrdersByYear(int year);
        public List<Order> SearchOrdersByStatus(OrderStatus status);
        public List<Order> SearchOrdersByProduct(int productId);
        public List<Order> ListOrders();
        public void DeleteOrdersByMonth(int month);
        public void DeleteOrdersByYear(int year);
        public void DeleteOrdersByStatus(OrderStatus status);
        public void DeleteOrdersByProduct(int productId);
    }
}
