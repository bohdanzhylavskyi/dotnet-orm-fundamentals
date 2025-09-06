using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF.Lib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Build configuration (so we can read appsettings.json)
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Read connection string
            var connectionString = config.GetConnectionString("DefaultConnection");

            // Configure DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using var context = new AppDbContext(optionsBuilder.Options);

            // Example: add data
            context.Products.Add(new Product()
            {
                Name = "iPhone 15",
                Description = "Latest Apple smartphone",
                Weight = 0.174m,
                Height = 14.700m,
                Length = 7.100m,
                Width = 0.750m
            });
            context.SaveChanges();
        }
    }
}
