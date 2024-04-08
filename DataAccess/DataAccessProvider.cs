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
        //
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

        //
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
    }
}
