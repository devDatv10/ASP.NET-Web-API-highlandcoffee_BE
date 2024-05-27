using System.Data;
using highlandcoffeeapp_BE.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Logging;

namespace highlandcoffeeapp_BE.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;
        private readonly ILogger<DataAccessProvider> _logger; // Khai báo ILogger

        public DataAccessProvider(PostgreSqlContext context,  ILogger<DataAccessProvider> logger)
        {
            _context = context;
            _logger = logger; // Khởi tạo ILogger
        }

        // function for account
        public void AddAccount(Account account)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "INSERT INTO accounts (username, password, personid, status) VALUES (@username, @password, @personid, @status)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("username", account.username));
                command.Parameters.Add(new NpgsqlParameter("password", account.password));
                command.Parameters.Add(new NpgsqlParameter("personid", account.personid));
                command.Parameters.Add(new NpgsqlParameter("status", account.status));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public void UpdateAccount(Account account)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "UPDATE accounts SET password = @password, personid = @personid, status = @status WHERE username = @username";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("username", account.username));
                command.Parameters.Add(new NpgsqlParameter("password", account.password));
                command.Parameters.Add(new NpgsqlParameter("personid", account.personid));
                command.Parameters.Add(new NpgsqlParameter("status", account.status));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public void DeleteAccount(string username)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "DELETE FROM accounts WHERE username = @username";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("username", username));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public Account GetAccountByUserName(string username)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM accounts WHERE username = @username";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("username", username));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Account
                        {
                            username = reader["username"].ToString(),
                            password = reader["password"].ToString(),
                            personid = reader["personid"].ToString(),
                            status = int.Parse(reader["status"].ToString())
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }

        public List<Account> GetAllAccounts()
        {
            var accounts = new List<Account>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM accounts";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        accounts.Add(new Account
                        {
                            username = reader["username"].ToString(),
                            password = reader["password"].ToString(),
                            personid = reader["personid"].ToString(),
                            status = int.Parse(reader["status"].ToString())
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return accounts;
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

        // Function for adding customer
        public void AddCustomer(Customer customer)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    // Sử dụng SELECT để gọi hàm add_new_customer
                    command.CommandText = @"
                        SELECT add_customer(@p_name, @p_phonenumber, @p_address, @p_point, @p_password)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_name", customer.name));
                    command.Parameters.Add(new NpgsqlParameter("p_phonenumber", customer.phonenumber));
                    command.Parameters.Add(new NpgsqlParameter("p_address", customer.address));
                    command.Parameters.Add(new NpgsqlParameter("p_point", customer.point));
                    command.Parameters.Add(new NpgsqlParameter("p_password", customer.password));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                // Log lỗi để biết nguyên nhân chính xác
                _logger.LogError(ex, "Error adding customer");
                throw;
            }
        }


        public void UpdateCustomer(Customer customer)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "update_customer";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new NpgsqlParameter("p_customerid", customer.customerid));
            command.Parameters.Add(new NpgsqlParameter("p_name", customer.name));
            command.Parameters.Add(new NpgsqlParameter("p_phonenumber", customer.phonenumber));
            command.Parameters.Add(new NpgsqlParameter("p_address", customer.address));
            command.Parameters.Add(new NpgsqlParameter("p_point", customer.point));
            command.Parameters.Add(new NpgsqlParameter("p_password", customer.password));

            _context.Database.OpenConnection();
            command.ExecuteNonQuery();
            _context.Database.CloseConnection();
        }
    }

    public void DeleteCustomer(string customerid)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "delete_customer";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

            _context.Database.OpenConnection();
            command.ExecuteNonQuery();
            _context.Database.CloseConnection();
        }
    }

    public Customer GetCustomerById(string customerid)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_customer_by_id(@p_customerid)";
            command.CommandType = CommandType.Text;

            command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Customer
                    {
                        customerid = reader["customerid"].ToString(),
                        name = reader["name"].ToString(),
                        phonenumber = reader["phonenumber"].ToString(),
                        address = reader["address"].ToString(),
                        point = int.Parse(reader["point"].ToString()),
                        password = reader["password"].ToString()
                    };
                }
            }
            _context.Database.CloseConnection();
        }
        return null;
    }

    public List<Customer> GetAllCustomers()
    {
        var customers = new List<Customer>();
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_all_customers()";
            command.CommandType = CommandType.Text;

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        customerid = reader["customerid"].ToString(),
                        name = reader["name"].ToString(),
                        phonenumber = reader["phonenumber"].ToString(),
                        address = reader["address"].ToString(),
                        point = int.Parse(reader["point"].ToString()),
                        password = reader["password"].ToString()
                    });
                }
            }
            _context.Database.CloseConnection();
        }
        return customers;
    }

        // function for category
        public void AddCategory(Category category)
    {
        try
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                    SELECT add_category(@p_categoryname, @p_description)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_categoryname", category.categoryname));
                command.Parameters.Add(new NpgsqlParameter("p_description", category.description));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding category");
            throw;
        }
    }

    public void UpdateCategory(Category category)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "update_category";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new NpgsqlParameter("p_categoryid", category.categoryid));
            command.Parameters.Add(new NpgsqlParameter("p_categoryname", category.categoryname));
            command.Parameters.Add(new NpgsqlParameter("p_description", category.description));

            _context.Database.OpenConnection();
            command.ExecuteNonQuery();
            _context.Database.CloseConnection();
        }
    }

    public void DeleteCategory(string categoryid)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "delete_category";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new NpgsqlParameter("p_categoryid", categoryid));

            _context.Database.OpenConnection();
            command.ExecuteNonQuery();
            _context.Database.CloseConnection();
        }
    }

    public Category GetCategoryById(string categoryid)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_category_by_id(@p_categoryid)";
            command.CommandType = CommandType.Text;

            command.Parameters.Add(new NpgsqlParameter("p_categoryid", categoryid));

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Category
                    {
                        categoryid = reader["categoryid"].ToString(),
                        categoryname = reader["categoryname"].ToString(),
                        description = reader["description"].ToString()
                    };
                }
            }
            _context.Database.CloseConnection();
        }
        return null;
    }

    public List<Category> GetAllCategories()
    {
        var categories = new List<Category>();
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_all_categories()";
            command.CommandType = CommandType.Text;

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(new Category
                    {
                        categoryid = reader["categoryid"].ToString(),
                        categoryname = reader["categoryname"].ToString(),
                        description = reader["description"].ToString()
                    });
                }
            }
            _context.Database.CloseConnection();
        }
        return categories;
    }


        // function for staff
        public void AddStaffRecord(Staff staff)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "add_staff";

                    cmd.Parameters.Add(new NpgsqlParameter("p_name", staff.name));
                    cmd.Parameters.Add(new NpgsqlParameter("p_phonenumber", staff.phonenumber));
                    cmd.Parameters.Add(new NpgsqlParameter("p_startday", staff.startday));
                    cmd.Parameters.Add(new NpgsqlParameter("p_salary", staff.salary));
                    cmd.Parameters.Add(new NpgsqlParameter("p_password", staff.password));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStaffRecord(Staff staff)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "update_staff";

                    cmd.Parameters.Add(new NpgsqlParameter("p_staffid", staff.id));
                    cmd.Parameters.Add(new NpgsqlParameter("p_name", staff.name));
                    cmd.Parameters.Add(new NpgsqlParameter("p_phonenumber", staff.phonenumber));
                    cmd.Parameters.Add(new NpgsqlParameter("p_startday", staff.startday));
                    cmd.Parameters.Add(new NpgsqlParameter("p_salary", staff.salary));
                    cmd.Parameters.Add(new NpgsqlParameter("p_password", staff.password));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStaffRecord(string id)
        {
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "delete_staff";

                    cmd.Parameters.Add(new NpgsqlParameter("p_staffid", id));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Staff GetStaffById(string id)
{
    using (var command = _context.Database.GetDbConnection().CreateCommand())
    {
        command.CommandText = "SELECT * FROM get_staff_by_id(@p_staffid)";
        command.CommandType = CommandType.Text;

        command.Parameters.Add(new NpgsqlParameter("p_staffid", id));

        _context.Database.OpenConnection();
        using (var reader = command.ExecuteReader())
        {
            if (reader.Read())
            {
                return new Staff
                {
                    id = reader["id"].ToString(),
                    name = reader["name"].ToString(),
                    phonenumber = reader["phonenumber"].ToString(),
                    startday = DateTime.Parse(reader["startday"].ToString()),
                    salary = int.Parse(reader["salary"].ToString()),
                    password = reader["password"].ToString()
                };
            }
        }
        _context.Database.CloseConnection();
    }
    return null;
}

public List<Staff> GetAllStaffs()
{
    var staffs = new List<Staff>();
    using (var command = _context.Database.GetDbConnection().CreateCommand())
    {
        command.CommandText = "SELECT * FROM get_all_staffs()";
        command.CommandType = CommandType.Text;

        _context.Database.OpenConnection();
        using (var reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                staffs.Add(new Staff
                {
                    id = reader["id"].ToString(),
                    name = reader["name"].ToString(),
                    phonenumber = reader["phonenumber"].ToString(),
                    startday = DateTime.Parse(reader["startday"].ToString()),
                    salary = int.Parse(reader["salary"].ToString()),
                    password = reader["password"].ToString()
                });
            }
        }
        _context.Database.CloseConnection();
    }
    return staffs;
}


        // function for product
        public void AddProduct(Product product)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                    SELECT add_product(@p_categoryid, @p_productname, @p_description, @p_size, @p_price, @p_unit, @p_image, @p_imagedetail)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_categoryid", product.categoryid));
                command.Parameters.Add(new NpgsqlParameter("p_productname", product.productname));
                command.Parameters.Add(new NpgsqlParameter("p_description", product.description));
                command.Parameters.Add(new NpgsqlParameter("p_size", product.size));
                command.Parameters.Add(new NpgsqlParameter("p_price", product.price));
                command.Parameters.Add(new NpgsqlParameter("p_unit", product.unit));
                command.Parameters.Add(new NpgsqlParameter("p_image", product.image));
                command.Parameters.Add(new NpgsqlParameter("p_imagedetail", product.imagedetail));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public void UpdateProduct(Product product)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "update_product";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_productid", product.productid));
                command.Parameters.Add(new NpgsqlParameter("p_categoryid", product.categoryid));
                command.Parameters.Add(new NpgsqlParameter("p_productname", product.productname));
                command.Parameters.Add(new NpgsqlParameter("p_description", product.description));
                command.Parameters.Add(new NpgsqlParameter("p_size", product.size));
                command.Parameters.Add(new NpgsqlParameter("p_price", product.price));
                command.Parameters.Add(new NpgsqlParameter("p_unit", product.unit));
                command.Parameters.Add(new NpgsqlParameter("p_image", product.image));
                command.Parameters.Add(new NpgsqlParameter("p_imagedetail", product.imagedetail));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public void DeleteProduct(string productid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "delete_product";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_productid", productid));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public Product GetProductById(string productid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_product_by_id(@p_productid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_productid", productid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            productid = reader["productid"].ToString(),
                            categoryid = reader["categoryid"].ToString(),
                            productname = reader["productname"].ToString(),
                            description = reader["description"].ToString(),
                            size = reader["size"].ToString(),
                            price = int.Parse(reader["p"].ToString()),
                            unit = reader["unit"].ToString(),
                            image = reader["image"] as byte[],
                            imagedetail = reader["imagedetail"] as byte[]
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }

        public List<Product> GetProductsByCategoryId(string categoryid)
        {
            var products = new List<Product>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_products_by_category_id(@p_categoryid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_categoryid",categoryid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            productid = reader["productid"].ToString(),
                            categoryid = reader["categoryid"].ToString(),
                            productname = reader["productname"].ToString(),
                            description = reader["description"].ToString(),
                            size = reader["size"].ToString(),
                            price = int.Parse(reader["price"].ToString()),
                            unit = reader["unit"].ToString(),
                            image = reader["image"] as byte[],
                            imagedetail = reader["imagedetail"] as byte[]
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return products;
        }

        public List<Product> GetAllProducts()
        {
            var products = new List<Product>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_products()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            productid = reader["productid"].ToString(),
                            categoryid = reader["categoryid"].ToString(),
                            productname = reader["productname"].ToString(),
                            description = reader["description"].ToString(),
                            size = reader["size"].ToString(),
                            price = int.Parse(reader["price"].ToString()),
                            unit = reader["unit"].ToString(),
                            image = reader["image"] as byte[],
                            imagedetail = reader["imagedetail"] as byte[]
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return products;
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
        public void AddFavorite(Favorite favorite)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = @"
                SELECT add_favorite(@p_customerid, @p_productid, @p_productname, @p_description, @p_size, @p_price, @p_unit, @p_image, @p_imagedetail)";
            command.CommandType = CommandType.Text;

            command.Parameters.Add(new NpgsqlParameter("p_customerid", favorite.customerid));
            command.Parameters.Add(new NpgsqlParameter("p_productid", favorite.productid));
            command.Parameters.Add(new NpgsqlParameter("p_productname", favorite.productname));
            command.Parameters.Add(new NpgsqlParameter("p_description", favorite.description));
            command.Parameters.Add(new NpgsqlParameter("p_size", favorite.size));
            command.Parameters.Add(new NpgsqlParameter("p_price", favorite.price));
            command.Parameters.Add(new NpgsqlParameter("p_unit", favorite.unit));
            command.Parameters.Add(new NpgsqlParameter("p_image", favorite.image ?? (object)DBNull.Value));
            command.Parameters.Add(new NpgsqlParameter("p_imagedetail", favorite.imagedetail ?? (object)DBNull.Value));

            _context.Database.OpenConnection();
            command.ExecuteNonQuery();
            _context.Database.CloseConnection();
        }
    }

    public void DeleteFavorite(string favoriteid)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = @"
                SELECT delete_favorite(@p_favoriteid)";
            command.CommandType = CommandType.Text;

            command.Parameters.Add(new NpgsqlParameter("p_favoriteid", favoriteid));

            _context.Database.OpenConnection();
            command.ExecuteNonQuery();
            _context.Database.CloseConnection();
        }
    }

    public List<Favorite> GetAllFavorites()
    {
        var favorites = new List<Favorite>();
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_all_favorites()";
            command.CommandType = CommandType.Text;

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    favorites.Add(new Favorite
                    {
                        favoriteid = reader["favoriteid"].ToString(),
                        customerid = reader["customerid"].ToString(),
                        productid = reader["productid"].ToString(),
                        productname = reader["productname"].ToString(),
                        description = reader["description"].ToString(),
                        size = reader["size"].ToString(),
                        price = int.Parse(reader["price"].ToString()),
                        unit = reader["unit"].ToString(),
                        image = reader["image"] as byte[],
                        imagedetail = reader["imagedetail"] as byte[]
                    });
                }
            }
            _context.Database.CloseConnection();
        }
        return favorites;
    }

    public Favorite GetFavoriteById(string favoriteid)
    {
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_favorite_by_id(@p_favoriteid)";
            command.CommandType = CommandType.Text;

            command.Parameters.Add(new NpgsqlParameter("p_favoriteid", favoriteid));

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Favorite
                    {
                        favoriteid = reader["favoriteid"].ToString(),
                        customerid = reader["customerid"].ToString(),
                        productid = reader["productid"].ToString(),
                        productname = reader["productname"].ToString(),
                        description = reader["description"].ToString(),
                        size = reader["size"].ToString(),
                        price = int.Parse(reader["price"].ToString()),
                        unit = reader["unit"].ToString(),
                        image = reader["image"] as byte[],
                        imagedetail = reader["imagedetail"] as byte[]
                    };
                }
            }
            _context.Database.CloseConnection();
        }
        return null;
    }

    public List<Favorite> GetFavoritesByCustomerId(string customerId)
    {
        var favorites = new List<Favorite>();
        using (var command = _context.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "SELECT * FROM get_favorites_by_customer_id(@p_customerid)";
            command.CommandType = CommandType.Text;

            command.Parameters.Add(new NpgsqlParameter("p_customerid", customerId));

            _context.Database.OpenConnection();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    favorites.Add(new Favorite
                    {
                        favoriteid = reader["favoriteid"].ToString(),
                        customerid = reader["customerid"].ToString(),
                        productid = reader["productid"].ToString(),
                        productname = reader["productname"].ToString(),
                        description = reader["description"].ToString(),
                        size = reader["size"].ToString(),
                        price = int.Parse(reader["price"].ToString()),
                        unit = reader["unit"].ToString(),
                        image = reader["image"] as byte[],
                        imagedetail = reader["imagedetail"] as byte[]
                    });
                }
            }
            _context.Database.CloseConnection();
        }
        return favorites;
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
    }
}
