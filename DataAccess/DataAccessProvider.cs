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

        public void DeleteAdminsRecord(string id)
        {
            var entity = _context.admins.FirstOrDefault(t => t.id == id);
            _context.admins.Remove(entity);
            _context.SaveChanges();
        }

        public Admin GetAdminsSingleRecord(string id)
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

        public void DeleteCategoriesRecord(string categoryid)
        {
            var entity = _context.categories.FirstOrDefault(t => t.categoryid == categoryid);
            _context.categories.Remove(entity);
            _context.SaveChanges();
        }

        public Category GetCategoriesSingleRecord(string categoryid)
        {
            return _context.categories.FirstOrDefault(t => t.categoryid == categoryid);
        }

        public List<Category> GetCategoriesRecords()
        {
            return _context.categories.ToList();
        }

        // function for staff
        public void AddStaffsRecord(Staff staff)
        {
            _context.staffs.Add(staff);
            _context.SaveChanges();
        }

        public void UpdateStaffsRecord(Staff staff)
        {
            _context.staffs.Update(staff);
            _context.SaveChanges();
        }

        public void DeleteStaffsRecord(int id)
        {
            var entity = _context.staffs.FirstOrDefault(t => t.id == id);
            _context.staffs.Remove(entity);
            _context.SaveChanges();
        }

        public Staff GetStaffsSingleRecord(int id)
        {
            return _context.staffs.FirstOrDefault(t => t.id == id);
        }

        public List<Staff> GetStaffsRecords()
        {
            return _context.staffs.ToList();
        }

        // function for product
        public void AddProductsRecord(Product product)
        {
            _context.products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProductsRecord(Product product)
        {
            _context.products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProductsRecord(int id)
        {
            var entity = _context.products.FirstOrDefault(t => t.id == id);
            _context.products.Remove(entity);
            _context.SaveChanges();
        }

        public Product GetProductsSingleRecord(int id)
        {
            return _context.products.FirstOrDefault(t => t.id == id);
        }

        public List<Product> GetProductsRecords()
        {
            return _context.products.ToList();
        }

        // function for coffee
        public void AddCoffeesRecord(Coffee coffee)
        {
            _context.coffees.Add(coffee);
            _context.SaveChanges();
        }

        public void UpdateCoffeesRecord(Coffee coffee)
        {
            _context.coffees.Update(coffee);
            _context.SaveChanges();
        }

        public void DeleteCoffeesRecord(int id)
        {
            var entity = _context.coffees.FirstOrDefault(t => t.id == id);
            _context.coffees.Remove(entity);
            _context.SaveChanges();
        }

        public Coffee GetCoffeesSingleRecord(int id)
        {
            return _context.coffees.FirstOrDefault(t => t.id == id);
        }

        public List<Coffee> GetCoffeesRecords()
        {
            return _context.coffees.ToList();
        }

        // function for tea
        public void AddTeasRecord(Tea tea)
        {
            _context.teas.Add(tea);
            _context.SaveChanges();
        }

        public void UpdateTeasRecord(Tea tea)
        {
            _context.teas.Update(tea);
            _context.SaveChanges();
        }
        
        public void DeleteTeasRecord(int id)
        {
            var entity = _context.teas.FirstOrDefault(t => t.id == id);
            _context.teas.Remove(entity);
            _context.SaveChanges();
        }

        public Tea GetTeasSingleRecord(int id)
        {
            return _context.teas.FirstOrDefault(t => t.id == id);
        }

        public List<Tea> GetTeasRecords()
        {
            return _context.teas.ToList();
        }

        // function for freeze
        public void AddFreezesRecord(Freeze freeze)
        {
            _context.freezes.Add(freeze);
            _context.SaveChanges();
        }

        public void UpdateFreezesRecord(Freeze freeze)
        {
            _context.freezes.Update(freeze);
            _context.SaveChanges();
        }

        public void DeleteFreezesRecord(int id)
        {
            var entity = _context.freezes.FirstOrDefault(t => t.id == id);
            _context.freezes.Remove(entity);
            _context.SaveChanges();
        }

        public Freeze GetFreezesSingleRecord(int id)
        {
            return _context.freezes.FirstOrDefault(t => t.id == id);
        }

        public List<Freeze> GetFreezesRecords()
        {
            return _context.freezes.ToList();
        }

        // function for bread
        public void AddBreadsRecord(Bread bread)
        {
            _context.breads.Add(bread);
            _context.SaveChanges();
        }

        public void UpdateBreadsRecord(Bread bread)
        {
            _context.breads.Update(bread);
            _context.SaveChanges();
        }

        public void DeleteBreadsRecord(int id)
        {
            var entity = _context.breads.FirstOrDefault(t => t.id == id);
            _context.breads.Remove(entity);
            _context.SaveChanges();
        }

        public Bread GetBreadsSingleRecord(int id)
        {
            return _context.breads.FirstOrDefault(t => t.id == id);
        }

        public List<Bread> GetBreadsRecords()
        {
            return _context.breads.ToList();
        }

        // function for Food
        public void AddFoodsRecord(Food food)
        {
            _context.foods.Add(food);
            _context.SaveChanges();
        }

        public void UpdateFoodsRecord(Food food)
        {
            _context.foods.Update(food);
            _context.SaveChanges();
        }

        public void DeleteFoodsRecord(int id)
        {
            var entity = _context.foods.FirstOrDefault(t => t.id == id);
            _context.foods.Remove(entity);
            _context.SaveChanges();
        }

        public Food GetFoodsSingleRecord(int id)
        {
            return _context.foods.FirstOrDefault(t => t.id == id);
        }

        public List<Food> GetFoodsRecords()
        {
            return _context.foods.ToList();
        }

        // function for other
        public void AddOthersRecord(Other other)
        {
            _context.others.Add(other);
            _context.SaveChanges();
        }

        public void UpdateOthersRecord(Other other)
        {
            _context.others.Update(other);
            _context.SaveChanges();
        }

        public void DeleteOthersRecord(int id)
        {
            var entity = _context.others.FirstOrDefault(t => t.id == id);
            _context.others.Remove(entity);
            _context.SaveChanges();
        }

        public Other GetOthersSingleRecord(int id)
        {
            return _context.others.FirstOrDefault(t => t.id == id);
        }

        public List<Other> GetOthersRecords()
        {
            return _context.others.ToList();
        }

        // function for popular
        public void AddPopularsRecord(Popular popular)
        {
            _context.populars.Add(popular);
            _context.SaveChanges();
        }

        public void UpdatePopularsRecord(Popular popular)
        {
            _context.populars.Update(popular);
            _context.SaveChanges();
        }

        public void DeletePopularsRecord(int id)
        {
            var entity = _context.populars.FirstOrDefault(t => t.id == id);
            _context.populars.Remove(entity);
            _context.SaveChanges();
        }

        public Popular GetPopularsSingleRecord(int id)
        {
            return _context.populars.FirstOrDefault(t => t.id == id);
        }

        public List<Popular> GetPopularsRecords()
        {
            return _context.populars.ToList();
        }

        // function for best sale
        public void AddBestSalesRecord(BestSale bestSale)
        {
            _context.bestsales.Add(bestSale);
            _context.SaveChanges();
        }

        public void UpdateBestSalesRecord(BestSale bestSale)
        {
            _context.bestsales.Update(bestSale);
            _context.SaveChanges();
        }

        public void DeleteBestSalesRecord(int id)
        {
            var entity = _context.bestsales.FirstOrDefault(t => t.id == id);
            _context.bestsales.Remove(entity);
            _context.SaveChanges();
        }

        public BestSale GetBestSalesSingleRecord(int id)
        {
            return _context.bestsales.FirstOrDefault(t => t.id == id);
        }

        public List<BestSale> GetBestSalesRecords()
        {
            return _context.bestsales.ToList();
        }

        // function for favorite
        public void AddFavoritesRecord(Favorite favorite)
        {
            _context.favorites.Add(favorite);
            _context.SaveChanges();
        }

        public void UpdateFavoritesRecord(Favorite favorite)
        {
            _context.favorites.Update(favorite);
            _context.SaveChanges();
        }

        public void DeleteFavoritesRecord(int id)
        {
            var entity = _context.favorites.FirstOrDefault(t => t.id == id);
            _context.favorites.Remove(entity);
            _context.SaveChanges();
        }

        public Favorite GetFavoritesSingleRecord(int id)
        {
            return _context.favorites.FirstOrDefault(t => t.id == id);
        }

        public List<Favorite> GetFavoritesRecords()
        {
            return _context.favorites.ToList();
        }

        // function for cart
        public void AddCartsRecord(Cart cart)
        {
            _context.carts.Add(cart);
            _context.SaveChanges();
        }

        public void UpdateCartsRecord(Cart cart)
        {
            _context.carts.Update(cart);
            _context.SaveChanges();
        }

        public void DeleteCartsRecord(int id)
        {
            var entity = _context.carts.FirstOrDefault(t => t.id == id);
            _context.carts.Remove(entity);
            _context.SaveChanges();
        }

        public Cart GetCartsSingleRecord(int id)
        {
            return _context.carts.FirstOrDefault(t => t.id == id);
        }

        public List<Cart> GetCartsRecords()
        {
            return _context.carts.ToList();
        }

        // function for order
        public void AddOrdersRecord(Order order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrdersRecord(Order order)
        {
            _context.orders.Update(order);
            _context.SaveChanges();
        }

        public void DeleteOrdersRecord(int id)
        {
            var entity = _context.orders.FirstOrDefault(t => t.id == id);
            _context.orders.Remove(entity);
            _context.SaveChanges();
        }

        public Order GetOrdersSingleRecord(int id)
        {
            return _context.orders.FirstOrDefault(t => t.id == id);
        }

        public List<Order> GetOrdersRecords()
        {
            return _context.orders.ToList();
        }

        // function for test
        public void AddTestsRecord(Test test)
        {
            _context.tests.Add(test);
            _context.SaveChanges();
        }

        public void UpdateTestsRecord(Test test)
        {
            _context.tests.Update(test);
            _context.SaveChanges();
        }

        public void DeleteTestsRecord(int id)
        {
            var entity = _context.tests.FirstOrDefault(t => t.id == id);
            _context.tests.Remove(entity);
            _context.SaveChanges();
        }

        public Test GetTestsSingleRecord(int id)
        {
            return _context.tests.FirstOrDefault(t => t.id == id);
        }

        public List<Test> GetTestsRecords()
        {
            return _context.tests.ToList();
        }

        // function for test1
        public void AddTest1sRecord(Test1 test1)
        {
            _context.test1s.Add(test1);
            _context.SaveChanges();
        }

        public void UpdateTest1sRecord(Test1 test1)
        {
            _context.test1s.Update(test1);
            _context.SaveChanges();
        }

        public void DeleteTest1sRecord(string id)
        {
            var entity = _context.test1s.FirstOrDefault(t => t.id == id);
            _context.test1s.Remove(entity);
            _context.SaveChanges();
        }

        public Test1 GetTest1sSingleRecord(string id)
        {
            return _context.test1s.FirstOrDefault(t => t.id == id);
        }

        public List<Test1> GetTest1sRecords()
        {
            return _context.test1s.ToList();
        }
    }
}
