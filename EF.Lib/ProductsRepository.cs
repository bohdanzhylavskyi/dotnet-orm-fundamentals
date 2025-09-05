using Core;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFLib
{
    public class ProductsRepository : IProductsRepository
    {
        private AppDbContext _dbContext;

        public ProductsRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreateProduct(Product product)
        {
            this._dbContext.Products.Add(product);
            this._dbContext.SaveChanges();
        }

        public Product? GetProduct(int productId)
        {
            return this._dbContext.Products.FirstOrDefault(p => p.Id == productId);
        }

        public void UpdateProduct(Product product)
        {
            this._dbContext.Products.Update(product);
            this._dbContext.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            this._dbContext.Products.Where(p => p.Id == productId).ExecuteDelete();
        }

        public List<Product> ListProducts()
        {
            return this._dbContext.Products.ToList();
        }
    }
}
