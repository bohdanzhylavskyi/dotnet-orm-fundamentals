using Core;
using Microsoft.EntityFrameworkCore;

namespace EF.Lib
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Order>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .IsRequired();

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
