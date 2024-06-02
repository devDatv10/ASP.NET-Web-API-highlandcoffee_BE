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
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(string adminid);
        Admin GetAdminById(string adminid);
        List<Admin> GetAllAdmins();

        // function for customer
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string customerid);
        Customer GetCustomerById(string customerid);
        List<Customer> GetAllCustomers();


        // function for category
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(string categoryid);
        Category GetCategoryById(string categoryid);
        List<Category> GetAllCategories();

        // function for cart
        void AddCart(CartDetail cartdetail);
        void UpdateCart(Cart cart);
        void DeleteCart(string cartid);
        Cart GetCartByCustomerId(string customerid);
        Cart GetCartById(string cartid);
        List<Cart> GetAllCart();

        // function for cart detail
        void AddCartDetail(CartDetail cartDetail);
        void UpdateCartDetail(CartDetail cartDetail);
        void DeleteCartDetailByCartId(string cartid);
        CartDetail GetCartDetailByCustomerId(string customerid);
        CartDetail GetCartDetailByCartId(string cartid);
        List<CartDetail> GetAllCartDetails();

        // function for staff
        void AddStaffRecord(Staff staff);
        void UpdateStaffRecord(Staff staff);
        void DeleteStaffRecord(string id);
        Staff GetStaffById(string id);
        List<Staff> GetAllStaffs();



        // function for product
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string productid);
        Product GetProductById(string productid);
        List<Product> GetAllProducts();
        List<Product> GetProductsByCategoryId(string categoryid);

        // function for favorite
        void AddFavorite(Favorite favorite);
        void DeleteFavorite(string favoriteid);
        List<Favorite> GetAllFavorites();
        Favorite GetFavoriteById(string favoriteid);
        List<Favorite> GetFavoritesByCustomerId(string customerid);

        // function for order
        void AddOrdersRecord(Order order);
        void UpdateOrdersRecord(Order order);
        void DeleteOrdersRecord(int id);
        Order GetOrdersSingleRecord(int id);
        List<Order> GetOrdersRecords();

        // function for order detail

        // function for comment
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(string commentid);
        Comment GetCommentById(string commentid);
        List<Comment> GetAllComments();
    }
}
