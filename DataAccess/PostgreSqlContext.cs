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

        public DbSet<Account> accounts { get; set; }
        public DbSet<Person> persons {get; set;}
        public DbSet<Admin> admins { get; set; }
        public DbSet<Staff> staffs { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Carousel> carousels {get; set;}
        public DbSet<CarouselNumber> carouselnumbers {get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartDetail> cartdetails { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Favorite> favorites { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetails { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Bill> bills { get; set; }

        // Method open connection
        public void OpenConnection()
        {
            if (Database.GetDbConnection().State != System.Data.ConnectionState.Open)
            {
                Database.GetDbConnection().Open();
            }
        }

        // Method close connection
        public void CloseConnection()
        {
            if (Database.GetDbConnection().State != System.Data.ConnectionState.Closed)
            {
                Database.GetDbConnection().Close();
            }
        }

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
