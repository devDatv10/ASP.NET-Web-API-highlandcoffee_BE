﻿using System.Data;
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

        public void DeleteCartDetailByCartId(string cartid)
        {
            // Implement logic for deleting cart details by cart ID here
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

        public CartDetail GetCartDetailByCartId(string cartid)
        {
            // Implement logic for getting a cart detail by cart ID here
            return null; // Placeholder, replace with actual implementation
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
