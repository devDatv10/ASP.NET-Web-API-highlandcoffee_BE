using highlandcoffeeapp_BE.Models;

namespace highlandcoffeeapp_BE.DataAccess
{
    public interface IDataAccessProvider
    {
        // function for account
        void AddAccount(Account account);
        void UpdateAccount(Account account);
        void DeleteAccount(string username);
        Account GetAccountByUserName(string username);
        List<Account> GetAllAccounts();

        // function for admin
        void AddAdminsRecord(Admin admin);
        void UpdateAdminsRecord(Admin admin);
        void DeleteAdminsRecord(string id);
        Admin GetAdminsSingleRecord(string id);
        List<Admin> GetAdminsRecords();

        // function for customer
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string id);
        Customer GetCustomerById(string id);
        List<Customer> GetAllCustomers();


        // function for category
        void AddCategoriesRecord(Category category);
        void UpdateCategoriesRecord(Category category);
        void DeleteCategoriesRecord(int id);
        Category GetCategoriesSingleRecord(int id);
        List<Category> GetCategoriesRecords();

        // function for cart
        void AddCartsRecord(Cart cart);
        void UpdateCartsRecord(Cart cart);
        void DeleteCartsRecord(int id);
        Cart GetCartsSingleRecord(int id);
        List<Cart> GetCartsRecords();

        // function for staff
        void AddStaffRecord(Staff staff);
        void UpdateStaffRecord(Staff staff);
        void DeleteStaffRecord(string id);
        Staff GetStaffById(string id);
        List<Staff> GetAllStaffs();



        // function for product
        void AddProductsRecord(Product product);
        void UpdateProductsRecord(Product product);
        void DeleteProductsRecord(int id);
        Product GetProductsSingleRecord(int id);
        List<Product> GetProductsRecords();

        // function for coffee
        void AddCoffeesRecord(Coffee coffee);
        void UpdateCoffeesRecord(Coffee coffee);
        void DeleteCoffeesRecord(int id);
        Coffee GetCoffeesSingleRecord(int id);
        List<Coffee> GetCoffeesRecords();

        // function for tea
        void AddTeasRecord(Tea tea);
        void UpdateTeasRecord(Tea tea);
        void DeleteTeasRecord(int id);
        Tea GetTeasSingleRecord(int id);
        List<Tea> GetTeasRecords();

        // function for freeze
        void AddFreezesRecord(Freeze freeze);
        void UpdateFreezesRecord(Freeze freeze);
        void DeleteFreezesRecord(int id);
        Freeze GetFreezesSingleRecord(int id);
        List<Freeze> GetFreezesRecords();

        // function for bread
        void AddBreadsRecord(Bread bread);
        void UpdateBreadsRecord(Bread bread);
        void DeleteBreadsRecord(int id);
        Bread GetBreadsSingleRecord(int id);
        List<Bread> GetBreadsRecords();

        // function for food
        void AddFoodsRecord(Food food);
        void UpdateFoodsRecord(Food food);
        void DeleteFoodsRecord(int id);
        Food GetFoodsSingleRecord(int id);
        List<Food> GetFoodsRecords();

        // function for other
        void AddOthersRecord(Other other);
        void UpdateOthersRecord(Other other);
        void DeleteOthersRecord(int id);
        Other GetOthersSingleRecord(int id);
        List<Other> GetOthersRecords();

        // function for popular
        void AddPopularsRecord(Popular popular);
        void UpdatePopularsRecord(Popular popular);
        void DeletePopularsRecord(int id);
        Popular GetPopularsSingleRecord(int id);
        List<Popular> GetPopularsRecords();

        // function for best sale
        void AddBestSalesRecord(BestSale bestSale);
        void UpdateBestSalesRecord(BestSale bestSale);
        void DeleteBestSalesRecord(int id);
        BestSale GetBestSalesSingleRecord(int id);
        List<BestSale> GetBestSalesRecords();

        // function for favorite
        void AddFavoritesRecord(Favorite favorite);
        void UpdateFavoritesRecord(Favorite favorite);
        void DeleteFavoritesRecord(int id);
        Favorite GetFavoritesSingleRecord(int id);
        List<Favorite> GetFavoritesRecords();

        // function for order
        void AddOrdersRecord(Order order);
        void UpdateOrdersRecord(Order order);
        void DeleteOrdersRecord(int id);
        Order GetOrdersSingleRecord(int id);
        List<Order> GetOrdersRecords();

        // function for order detail
    }
}
