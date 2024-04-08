using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }
        // function for admin
        public void AddAdminsRecord(Admin admin)
        {
            _context.admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateAdminsRecord(Admin admin)
        {
            _context.admins.Update(admin);
            _context.SaveChanges();
        }

        public void DeleteAdminsRecord(int id)
        {
            var entity = _context.admins.FirstOrDefault(t => t.id == id);
            _context.admins.Remove(entity);
            _context.SaveChanges();
        }

        public Admin GetAdminsSingleRecord(int id)
        {
            return _context.admins.FirstOrDefault(t => t.id == id);
        }

        public List<Admin> GetAdminsRecords()
        {
            return _context.admins.ToList();
        }

        // function for customer
        public void AddCustomersRecord(Customer customer)
        {
            _context.customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomersRecord(Customer customer)
        {
            _context.customers.Update(customer);
            _context.SaveChanges();
        }

        public void DeleteCustomersRecord(int id)
        {
            var entity = _context.customers.FirstOrDefault(t => t.id == id);
            _context.customers.Remove(entity);
            _context.SaveChanges();
        }

        public Customer GetCustomersSingleRecord(int id)
        {
            return _context.customers.FirstOrDefault(t => t.id == id);
        }

        public List<Customer> GetCustomersRecords()
        {
            return _context.customers.ToList();
        }

        // function for category
        public void AddCategoriesRecord(Category category)
        {
            _context.categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateCategoriesRecord(Category category)
        {
            _context.categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategoriesRecord(int id)
        {
            var entity = _context.categories.FirstOrDefault(t => t.id == id);
            _context.categories.Remove(entity);
            _context.SaveChanges();
        }

        public Category GetCategoriesSingleRecord(int id)
        {
            return _context.categories.FirstOrDefault(t => t.id == id);
        }

        public List<Category> GetCategoriesRecords()
        {
            return _context.categories.ToList();
        }
    }
}
