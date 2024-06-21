using System.Data;
using highlandcoffeeapp_BE.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;

namespace highlandcoffeeapp_BE.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;
        private readonly ILogger<DataAccessProvider> _logger; // Khai báo ILogger

        public DataAccessProvider(PostgreSqlContext context, ILogger<DataAccessProvider> logger)
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
        // Function for adding admin
        public void AddAdmin(Admin admin)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    // Sử dụng SELECT để gọi hàm add_admin
                    command.CommandText = @"
                SELECT add_admin(@p_name, @p_phonenumber, @p_shift, @p_password)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_name", admin.name));
                    command.Parameters.Add(new NpgsqlParameter("p_phonenumber", admin.phonenumber));
                    command.Parameters.Add(new NpgsqlParameter("p_shift", admin.shift));
                    command.Parameters.Add(new NpgsqlParameter("p_password", admin.password));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                // Log lỗi để biết nguyên nhân chính xác
                _logger.LogError(ex, "Error adding admin");
                throw;
            }
        }

        // Function for updating admin
        public void UpdateAdmin(Admin admin)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "update_admin";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_adminid", admin.adminid));
                command.Parameters.Add(new NpgsqlParameter("p_name", admin.name));
                command.Parameters.Add(new NpgsqlParameter("p_phonenumber", admin.phonenumber));
                command.Parameters.Add(new NpgsqlParameter("p_shift", admin.shift));
                command.Parameters.Add(new NpgsqlParameter("p_password", admin.password));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        // Function for deleting admin
        public void DeleteAdmin(string adminid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "delete_admin";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new NpgsqlParameter("p_adminid", adminid));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        // Function for getting admin by id
        public Admin GetAdminById(string adminid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_admin_by_id(@p_adminid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_adminid", adminid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            adminid = reader["adminid"].ToString(),
                            name = reader["name"].ToString(),
                            phonenumber = reader["phonenumber"].ToString(),
                            shift = int.Parse(reader["shift"].ToString()),
                            password = reader["password"].ToString()
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }

        // Function for getting all admins
        public List<Admin> GetAllAdmins()
        {
            var admins = new List<Admin>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_admins()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        admins.Add(new Admin
                        {
                            adminid = reader["adminid"].ToString(),
                            name = reader["name"].ToString(),
                            phonenumber = reader["phonenumber"].ToString(),
                            shift = int.Parse(reader["shift"].ToString()),
                            password = reader["password"].ToString()
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return admins;
        }


        // Function for customer
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
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                    SELECT update_customer(
                        @p_customerid,
                        @p_name,
                        @p_phonenumber,
                        @p_address,
                        @p_point,
                        @p_password
                        )";
                    command.CommandType = CommandType.Text;

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                throw;
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
                            password = reader["password"].ToString(),
                            status = int.Parse(reader["status"].ToString())
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return customers;
        }

        public void ActiveAccountCustomer(string personid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT active_account_customer(@p_personid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_personid", personid));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu cần
                throw new Exception("Error activating account", ex);
            }
        }

        public void BlockAccountCustomer(string personid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT block_account_customer(@p_personid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_personid", personid));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                // Xử lý exception nếu cần
                throw new Exception("Error blocking account", ex);
            }
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
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                    SELECT update_category(
                        @p_categoryid,
                        @p_categoryname,
                        @p_description)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_categoryid", category.categoryid));
                    command.Parameters.Add(new NpgsqlParameter("p_categoryname", category.categoryname));
                    command.Parameters.Add(new NpgsqlParameter("p_description", category.description));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public void DeleteCategory(string categoryid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                SELECT delete_category(@p_categoryid)";
                command.CommandType = CommandType.Text;

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
        public void AddStaff(Staff staff)
        {
            try
            {
                // Loại bỏ các ký tự dư \r từ các thuộc tính của staff
                staff.name = staff.name?.Replace("\r", "");
                staff.phonenumber = staff.phonenumber?.Replace("\r", "");
                staff.password = staff.password?.Replace("\r", "");

                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    // Sử dụng SELECT để gọi hàm add_staff
                    command.CommandText = @"
            SELECT add_staff(
                @p_name,
                @p_phonenumber,
                @p_password,
                @p_startday::date,
                @p_salary
            )";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_name", staff.name));
                    command.Parameters.Add(new NpgsqlParameter("p_phonenumber", staff.phonenumber));
                    command.Parameters.Add(new NpgsqlParameter("p_password", staff.password));
                    command.Parameters.Add(new NpgsqlParameter("p_startday", staff.startday == default(DateTime) ? (object)DBNull.Value : staff.startday));
                    command.Parameters.Add(new NpgsqlParameter("p_salary", staff.salary));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                // Log lỗi để biết nguyên nhân chính xác
                _logger.LogError(ex, "Error adding staff");
                throw;
            }
        }




        public void UpdateStaff(Staff staff)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT update_staff(
                    @p_staffid,
                    @p_name,
                    @p_phonenumber,
                    @p_startday,
                    @p_salary,
                    @p_password
                )";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_staffid", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = staff.staffid });
                    command.Parameters.Add(new NpgsqlParameter("p_name", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = staff.name });
                    command.Parameters.Add(new NpgsqlParameter("p_phonenumber", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = staff.phonenumber });
                    command.Parameters.Add(new NpgsqlParameter("p_password", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = staff.password });
                    command.Parameters.Add(new NpgsqlParameter("p_startday", NpgsqlTypes.NpgsqlDbType.Date) { Value = (object)staff.startday ?? DBNull.Value });
                    command.Parameters.Add(new NpgsqlParameter("p_salary", NpgsqlTypes.NpgsqlDbType.Integer) { Value = staff.salary });

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                // Log lỗi để biết nguyên nhân chính xác
                _logger.LogError(ex, "Error updating staff");
                throw;
            }
        }


        public void DeleteStaff(string staffid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                SELECT delete_staff(@p_staffid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_staffid", staffid));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public Staff GetStaffById(string staffid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_staff_by_id(@p_staffid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_staffid", staffid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Staff
                        {
                            staffid = reader["staffid"].ToString(),
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
                            staffid = reader["staffid"].ToString(),
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
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                    SELECT update_product(
                        @p_productid,
                        @p_categoryid,
                        @p_productname,
                        @p_description,
                        @p_size,
                        @p_price,
                        @p_unit,
                        @p_image,
                        @p_imagedetail)";
                    command.CommandType = CommandType.Text;

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product");
                throw;
            }
        }

        public void DeleteProduct(string productid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                SELECT delete_product(@p_productid)";
                command.CommandType = CommandType.Text;

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

        public List<Product> GetProductsByCategoryId(string categoryid)
        {
            var products = new List<Product>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_products_by_category_id(@p_categoryid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_categoryid", categoryid));

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

        public List<Favorite> GetFavoritesByCustomerId(string customerid)
        {
            var favorites = new List<Favorite>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_favorites_by_customer_id(@p_customerid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

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
        public void AddCart(CartDetail cartDetail)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                    SELECT add_cart(@p_customerid, @p_productid, @p_quantity, @p_price, @p_productname, @p_size, @p_image)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_customerid", cartDetail.customerid));
                    command.Parameters.Add(new NpgsqlParameter("p_productid", cartDetail.productid));
                    command.Parameters.Add(new NpgsqlParameter("p_quantity", cartDetail.quantity));
                    command.Parameters.Add(new NpgsqlParameter("p_price", cartDetail.totalprice));
                    command.Parameters.Add(new NpgsqlParameter("p_productname", cartDetail.productname));
                    command.Parameters.Add(new NpgsqlParameter("p_size", cartDetail.size));
                    command.Parameters.Add(new NpgsqlParameter("p_image", cartDetail.image));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding cart");
                throw;
            }
        }

        // Method for getting cart by customer ID
        public Cart GetCartByCustomerId(string customerid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT * FROM get_cart_by_customerid(@p_customerid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var cart = new Cart
                            {
                                cartid = reader.GetString(0),
                                customerid = reader.GetString(1)
                            };

                            // Assuming one cart per customer
                            return cart;
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart by customer ID");
                throw;
            }

            return null;
        }

        public Cart GetCartById(string cartid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_cart_by_id(@p_cartid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_cartid", cartid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Cart
                        {
                            cartid = reader["cartid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;

        }

        public void UpdateCart(Cart cart)
        {
            // Implement logic for updating a cart here
        }

        public void DeleteCart(string cartid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                SELECT delete_cart(@p_cartid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_cartid", cartid));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public List<Cart> GetAllCart()
        {
            var carts = new List<Cart>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_cart()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carts.Add(new Cart
                        {
                            cartid = reader["cartid"].ToString(),
                            customerid = reader["customerid"].ToString()
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return carts;
        }


        // Function for cart details
        public void AddCartDetail(CartDetail cartDetail)
        {
            // Implement logic for adding a cart detail here
        }

        public void UpdateCartDetail(CartDetail cartDetail)
        {
            // Implement logic for updating a cart detail here
        }

        public void DeleteCartDetail(string cartdetailid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
        SELECT remove_item_from_cart(@p_cartdetailid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_cartdetailid", cartdetailid));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }


        public CartDetail GetCartDetailByCustomerId(string customerid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT * FROM get_cart_detail_by_customerid(@p_customerid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cartDetail = new CartDetail
                            {
                                cartid = reader.GetString(0),
                                customerid = reader.GetString(1),
                                cartdetailid = reader.GetString(2),
                                productid = reader.GetString(3),
                                quantity = reader.GetInt32(4),
                                totalprice = reader.GetInt32(5),
                                productname = reader.GetString(6),
                                size = reader.GetString(7),
                                image = reader.IsDBNull(8) ? null : (byte[])reader[8]
                            };

                            return cartDetail;
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting cart details by customer ID");
                throw;
            }

            return null;
        }

        public CartDetail GetCartDetailByCartDetailId(string cartdetailid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_cart_detail_by_cartdetailid(@p_cartdetailid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_cartdetailid", cartdetailid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CartDetail
                        {
                            cartid = reader["cartid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            cartdetailid = reader["cartdetailid"].ToString(),
                            productid = reader["productid"].ToString(),
                            quantity = Convert.ToInt32(reader["quantity"]),
                            totalprice = Convert.ToInt32(reader["totalprice"]),
                            productname = reader["productname"].ToString(),
                            size = reader["size"].ToString(),
                            image = reader["image"] as byte[]
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }


        public List<CartDetail> GetAllCartDetails()
        {
            var cartDetails = new List<CartDetail>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_cartdetail()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cartDetails.Add(new CartDetail
                        {
                            cartid = reader["cartid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            cartdetailid = reader["cartdetailid"].ToString(),
                            productid = reader["productid"].ToString(),
                            quantity = Convert.ToInt32(reader["quantity"]),
                            totalprice = Convert.ToInt32(reader["totalprice"]),
                            productname = reader["productname"].ToString(),
                            size = reader["size"].ToString(),
                            image = (byte[])reader["image"]
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return cartDetails;
        }


        // function for order
        public void AddOrder(OrderDetail orderdetail)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
            SELECT public.add_order(
                @p_customerid,
                @p_paymentmethod,
                @p_cartid,
                @p_customername,
                @p_address,
                @p_phonenumber)";
                    command.CommandType = CommandType.Text;

                    // Thêm các tham số
                    command.Parameters.Add(new NpgsqlParameter("p_customerid", orderdetail.customerid));
                    command.Parameters.Add(new NpgsqlParameter("p_paymentmethod", orderdetail.paymentmethod));
                    command.Parameters.Add(new NpgsqlParameter("p_cartid", orderdetail.cartid));
                    command.Parameters.Add(new NpgsqlParameter("p_customername", orderdetail.customername));
                    command.Parameters.Add(new NpgsqlParameter("p_address", orderdetail.address));
                    command.Parameters.Add(new NpgsqlParameter("p_phonenumber", orderdetail.phonenumber));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding order");
                throw;
            }
        }



        public void UpdateOrder(Order order)
        {
            // Implement logic for adding a cart detail here
        }

        public void DeleteOrder(string orderid)
        {
            // Implement logic for adding a cart detail here
        }

        public Order GetOrderById(string orderid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_order_by_orderid(@p_orderid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_orderid", orderid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Order
                        {
                            orderid = reader["orderid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            staffid = reader["staffid"].ToString(),
                            date = DateTime.Parse(reader["date"].ToString()),
                            paymentmethod = reader["paymentmethod"].ToString(),
                            status = int.Parse(reader["status"].ToString()),
                            totalprice = int.Parse(reader["totalprice"].ToString())
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }


        public List<Order> GetOrderByCustomerId(string customerid)
        {
            List<Order> orders = new List<Order>();
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT * FROM get_order_by_customerid(@p_customerid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order order = new Order
                            {
                                orderid = reader["orderid"].ToString(),
                                customerid = reader["customerid"].ToString(),
                                staffid = reader["staffid"] != DBNull.Value ? reader["staffid"].ToString() : null,
                                date = reader.GetDateTime(reader.GetOrdinal("date")),
                                paymentmethod = reader["paymentmethod"].ToString(),
                                status = reader.GetInt32(reader.GetOrdinal("status")),
                                totalprice = reader.GetInt32(reader.GetOrdinal("totalprice"))
                            };
                            orders.Add(order);
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting orders by customer ID");
                throw;
            }

            return orders;
        }



        public List<Order> GetAllOrders()
        {
            var orders = new List<Order>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_order()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            orderid = reader["orderid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            staffid = reader["staffid"].ToString(),
                            date = DateTime.Parse(reader["date"].ToString()),
                            paymentmethod = reader["paymentmethod"].ToString(),
                            status = int.Parse(reader["status"].ToString()),
                            totalprice = int.Parse(reader["totalprice"].ToString())
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return orders;
        }

        // function confirm order
        public void ConfirmOrder(string orderid, string staffid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT confirm_order(
                    @p_orderid,
                    @p_staffid
                )";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_orderid", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = orderid });
                    command.Parameters.Add(new NpgsqlParameter("p_staffid", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = staffid });

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error confirming order");
                throw;
            }
        }

        // function cancel order
        public void CancelOrder(string orderid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT cancel_order(@p_orderid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_orderid", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = orderid });

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error cancelling order");
                throw;
            }
        }

        public void PrintBill(string orderid, string staffid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT print_bill(@p_orderid, @p_staffid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_orderid", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = orderid });
                    command.Parameters.Add(new NpgsqlParameter("p_staffid", NpgsqlTypes.NpgsqlDbType.Varchar) { Value = staffid });

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error printing bill");
                throw;
            }
        }


        // function for order detail
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            // Implement logic for adding a cart detail here
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            // Implement logic for adding a cart detail here
        }

        public void DeleteOrderDetail(string orderdetailid)
        {
            // Implement logic for adding a cart detail here
        }

        public OrderDetail GetOrderDetailById(string orderdetailid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_orderdetail_by_orderdetailid(@p_orderdetailid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_orderdetailid", orderdetailid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OrderDetail
                        {
                            orderdetailid = reader["orderdetailid"].ToString(),
                            orderid = reader["orderid"].ToString(),
                            productid = reader["productid"].ToString(),
                            quantity = int.Parse(reader["quantity"].ToString()),
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }

        public List<OrderDetail> GetOrderDetailByOrderId(string orderid)
        {
            var orderDetails = new List<OrderDetail>();
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
            SELECT * FROM get_orderdetail_by_orderid(@p_orderid)";
                    command.CommandType = CommandType.Text;

                    var parameter = new NpgsqlParameter("p_orderid", NpgsqlTypes.NpgsqlDbType.Varchar);
                    parameter.Value = orderid;
                    command.Parameters.Add(parameter);

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetail = new OrderDetail
                            {
                                orderdetailid = reader["orderdetailid"].ToString(),
                                orderid = reader["orderid"].ToString(),
                                staffid = reader.IsDBNull(reader.GetOrdinal("staffid")) ? null : reader["staffid"].ToString(),
                                customerid = reader["customerid"].ToString(),
                                productid = reader["productid"].ToString(),
                                productname = reader["productname"].ToString(),
                                quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                size = reader["size"].ToString(),
                                image = reader.IsDBNull(reader.GetOrdinal("image")) ? null : (byte[])reader["image"],
                                intomoney = reader.GetInt32(reader.GetOrdinal("intomoney")),
                                totalprice = reader.GetInt32(reader.GetOrdinal("totalprice")),
                                date = reader.GetDateTime(reader.GetOrdinal("date")),
                                paymentmethod = reader["paymentmethod"].ToString(),
                                status = reader.GetInt32(reader.GetOrdinal("status")),
                                customername = reader["customername"].ToString(),
                                address = reader["address"].ToString(),
                                phonenumber = reader["phonenumber"].ToString()
                            };
                            orderDetails.Add(orderDetail);
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order details by order ID");
                throw;
            }

            return orderDetails;
        }



        public OrderDetail GetOrderDetailByCustomerId(string customerid)
        {
            var orderDetails = new List<OrderDetail>();
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT * FROM get_order_detail_by_customerid(@p_customerid)";
                    command.CommandType = CommandType.Text;

                    command.Parameters.Add(new NpgsqlParameter("p_customerid", customerid));

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetail = new OrderDetail
                            {
                                orderdetailid = reader["orderdetailid"].ToString(),
                                orderid = reader["orderid"].ToString(),
                                staffid = reader.IsDBNull(2) ? null : reader["staffid"].ToString(),
                                customerid = reader["customerid"].ToString(),
                                productid = reader["productid"].ToString(),
                                productname = reader["productname"].ToString(),
                                quantity = reader.GetInt32(6),
                                size = reader["size"].ToString(),
                                image = reader.IsDBNull(8) ? null : (byte[])reader["image"],
                                intomoney = reader.GetInt32(9),
                                totalprice = reader.GetInt32(9),
                                date = reader.GetDateTime(10),
                                paymentmethod = reader["paymentmethod"].ToString(),
                                status = reader.GetInt32(12),
                                customername = reader["customername"].ToString(),
                                address = reader["address"].ToString(),
                                phonenumber = reader["phonenumber"].ToString()
                            };

                            orderDetails.Add(orderDetail);
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting order details by customer ID");
                throw;
            }

            return null;
        }


        public List<OrderDetail> GetAllOrderDetails()
        {
            var orderDetails = new List<OrderDetail>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_orderdetail()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        orderDetails.Add(new OrderDetail
                        {
                            orderdetailid = reader["orderdetailid"].ToString(),
                            orderid = reader["orderid"].ToString(),
                            staffid = reader["staffid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            productid = reader["productid"].ToString(),
                            productname = reader["productname"].ToString(),
                            quantity = int.Parse(reader["quantity"].ToString()),
                            size = reader["size"].ToString(),
                            image = reader["image"] as byte[],
                            intomoney = int.Parse(reader["intomoney"].ToString()),
                            totalprice = int.Parse(reader["totalprice"].ToString()),
                            date = DateTime.Parse(reader["date"].ToString()),
                            paymentmethod = reader["paymentmethod"].ToString(),
                            status = int.Parse(reader["status"].ToString()),
                            customername = reader["customername"].ToString(),
                            address = reader["address"].ToString(),
                            phonenumber = reader["phonenumber"].ToString(),
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return orderDetails;
        }





        // function for order comment
        public void AddComment(Comment comment)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
        SELECT add_comment(@p_customerid, @p_customername, @p_titlecomment, @p_contentcomment, @_image, @p_status)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_customerid", comment.customerid));
                command.Parameters.Add(new NpgsqlParameter("p_customername", comment.customername));
                command.Parameters.Add(new NpgsqlParameter("p_titlecomment", comment.titlecomment));
                command.Parameters.Add(new NpgsqlParameter("p_contentcomment", comment.contentcomment));
                command.Parameters.Add(new NpgsqlParameter("_image", comment.image));
                command.Parameters.Add(new NpgsqlParameter("p_status", comment.status));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }


        public void UpdateComment(Comment comment)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                SELECT update_comment(@p_commentid, @p_customerid, @p_customername, @p_titlecomment, @p_contentcomment, @p_date, @_image, @p_status)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_commentid", comment.commentid));
                command.Parameters.Add(new NpgsqlParameter("p_customerid", comment.customerid));
                command.Parameters.Add(new NpgsqlParameter("p_customername", comment.customername));
                command.Parameters.Add(new NpgsqlParameter("p_titlecomment", comment.titlecomment));
                command.Parameters.Add(new NpgsqlParameter("p_contentcomment", comment.contentcomment));
                command.Parameters.Add(new NpgsqlParameter("p_date", comment.date));
                command.Parameters.Add(new NpgsqlParameter("_image", comment.image));
                command.Parameters.Add(new NpgsqlParameter("p_status", comment.status));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public void DeleteComment(string commentid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                SELECT delete_comment(@p_commentid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_commentid", commentid));

                _context.Database.OpenConnection();
                command.ExecuteNonQuery();
                _context.Database.CloseConnection();
            }
        }

        public Comment GetCommentById(string commentid)
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_comment_by_id(@p_commentid)";
                command.CommandType = CommandType.Text;

                command.Parameters.Add(new NpgsqlParameter("p_commentid", commentid));

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Comment
                        {
                            commentid = reader["commentid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            customername = reader["customername"].ToString(),
                            titlecomment = reader["titlecomment"].ToString(),
                            contentcomment = reader["contentcomment"].ToString(),
                            date = DateTime.Parse(reader["date"].ToString()),
                            image = reader["image"] as byte[],
                            status = int.Parse(reader["status"].ToString())
                        };
                    }
                }
                _context.Database.CloseConnection();
            }
            return null;
        }

        public List<Comment> GetAllComments()
        {
            var comments = new List<Comment>();
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT * FROM get_all_comments()";
                command.CommandType = CommandType.Text;

                _context.Database.OpenConnection();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            commentid = reader["commentid"].ToString(),
                            customerid = reader["customerid"].ToString(),
                            customername = reader["customername"].ToString(),
                            titlecomment = reader["titlecomment"].ToString(),
                            contentcomment = reader["contentcomment"].ToString(),
                            date = DateTime.Parse(reader["date"].ToString()),
                            image = reader["image"] as byte[],
                            status = int.Parse(reader["status"].ToString())
                        });
                    }
                }
                _context.Database.CloseConnection();
            }
            return comments;
        }

        // function for bill
        public void AddBill(Bill bill)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
            SELECT public.add_bill(
                @p_orderid,
                @p_staffid)";
                    command.CommandType = CommandType.Text;

                    // Add parameters
                    command.Parameters.Add(new NpgsqlParameter("p_orderid", bill.orderid));
                    command.Parameters.Add(new NpgsqlParameter("p_staffid", bill.staffid));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding bill");
                throw;
            }
        }


        public void DeleteBill(string billid)
        {
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT public.delete_bill(@p_billid)";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("p_billid", billid));

                    _context.Database.OpenConnection();
                    command.ExecuteNonQuery();
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting bill");
                throw;
            }
        }

        public void UpdateBill(Bill bill)
        {
            _context.bills.Update(bill);
            _context.SaveChanges();
        }

        public List<Bill> GetAllBills()
        {
            var result = new List<Bill>();
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM public.get_all_bill()";
                    command.CommandType = CommandType.Text;

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bill = new Bill
                            {
                                billid = reader.GetString(0),
                                orderid = reader.GetString(1),
                                staffid = reader.GetString(2),
                                customerid = reader.GetString(3),
                                date = reader.GetDateTime(4),
                                paymentmethod = reader.GetString(5),
                                totalprice = reader.GetInt32(6),
                                discountcode = reader.GetInt32(7),
                                status = reader.GetInt32(8),
                                customername = reader.GetString(9),
                                staffname = reader.GetString(10),
                                address = reader.GetString(11),
                                phonenumber = reader.GetString(12)
                            };
                            result.Add(bill);
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all bills");
                throw;
            }

            return result;
        }

        public Bill GetBillById(string billid)
        {
            Bill result = null;
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT * FROM public.get_bill_by_id(@p_billid)";
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("p_billid", billid));

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = new Bill
                            {
                                billid = reader.GetString(0),
                                orderid = reader.GetString(1),
                                staffid = reader.GetString(2),
                                customerid = reader.GetString(3),
                                date = reader.GetDateTime(4),
                                paymentmethod = reader.GetString(5),
                                totalprice = reader.GetInt32(6),
                                discountcode = reader.GetInt32(7),
                                status = reader.GetInt32(8),
                                customername = reader.GetString(9),
                                staffname = reader.GetString(10),
                                address = reader.GetString(11),
                                phonenumber = reader.GetString(12)
                            };
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bill by id");
                throw;
            }

            return result;
        }

        public List<Bill> GetBillByOrderId(string orderid)
        {
            var bills = new List<Bill>();
            try
            {
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                        SELECT * FROM public.get_bill_by_orderid(@p_orderid)";
                    command.CommandType = CommandType.Text;

                    var parameter = new NpgsqlParameter("p_orderid", NpgsqlTypes.NpgsqlDbType.Varchar);
                    parameter.Value = orderid;
                    command.Parameters.Add(parameter);

                    _context.Database.OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bill = new Bill
                            {
                                billid = reader["billid"].ToString(),
                                orderid = reader["orderid"].ToString(),
                                totalprice = reader.GetInt32(reader.GetOrdinal("totalprice")),
                                discountcode = reader.GetInt32(reader.GetOrdinal("discountcode")),
                                status = reader.GetInt32(reader.GetOrdinal("status")),
                                staffid = reader["staffid"].ToString(),
                                staffname = reader["staffname"].ToString(),
                                customerid = reader["customerid"].ToString(),
                                customername = reader["customername"].ToString(),
                                date = reader.GetDateTime(reader.GetOrdinal("date")),
                                address = reader["address"].ToString(),
                                phonenumber = reader["phonenumber"].ToString(),
                                paymentmethod = reader["paymentmethod"].ToString()
                            };
                            bills.Add(bill);
                        }
                    }
                    _context.Database.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bills by order ID");
                throw;
            }

            return bills;
        }
    }
}
