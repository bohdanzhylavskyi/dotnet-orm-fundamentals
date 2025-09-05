namespace Core
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Weight { get; set; }
        public required decimal Height { get; set; }
        public required decimal Width { get; set; }
        public required decimal Length { get; set; }
    }

    public interface IProductsRepository
    {
        public void CreateProduct(Product product);
        public Product? GetProduct(int productId);
        public void UpdateProduct(Product product);
        public void DeleteProduct(int productId);
        public List<Product> ListProducts();
    }
}
