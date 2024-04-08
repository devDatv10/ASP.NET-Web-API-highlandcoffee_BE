using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.DataAccess
{
    public interface IDataAccessProvider
    {
        // function for admin
        void AddAdminsRecord(Admin admin);
        void UpdateAdminsRecord(Admin admin);
        void DeleteAdminsRecord(int id);
        Admin GetAdminsSingleRecord(int id);
        List<Admin> GetAdminsRecords();

        // function for customer
        void AddCustomersRecord(Customer customer);
        void UpdateCustomersRecord(Customer customer);
        void DeleteCustomersRecord(int id);
        Customer GetCustomersSingleRecord(int id);
        List<Customer> GetCustomersRecords();
    }
}
