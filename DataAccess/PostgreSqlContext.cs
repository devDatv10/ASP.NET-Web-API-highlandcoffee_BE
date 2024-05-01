using highlandcoffeeapp_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace highlandcoffeeapp_BE.DataAccess
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<Admin> admins { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Coffee> coffees { get; set; }
        public DbSet<Tea> teas { get; set; }
        public DbSet<Freeze> freezes { get; set; }
        public DbSet<Bread> breads { get; set; }
        public DbSet<Food> foods { get; set; }
        public DbSet<Other> others { get; set; }
        public DbSet<Popular> populars { get; set; }
        public DbSet<BestSale> bestsales { get; set; }
        public DbSet<Favorite> favorites { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }

        public DbSet<Test> tests { get; set; }
        public DbSet<Test1> test1s { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }
    }
}
