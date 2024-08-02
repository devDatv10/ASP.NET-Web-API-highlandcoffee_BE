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

        // function for person
        Person GetPersonById(string personid);
        List<Person> GetAllPersons();

        void UpdateStaffToAdmin(string personid);
        void UpdateAdminToStaff(string personid);
        Person GetRoleByPersonId(string personid);

        // function for admin
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(string adminid);
        Admin GetAdminById(string adminid);
        List<Admin> GetAllAdmins();

        // function for staff
        void AddStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(string staffid);
        Staff GetStaffById(string staffid);
        List<Staff> GetAllStaffs();

        // function for customer
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(string customerid);
        Customer GetCustomerById(string customerid);
        List<Customer> GetAllCustomers();

        // function active and block account customer
        void ActiveAccountCustomer(string personid);
        void BlockAccountCustomer(string personid);

        // function for carousel
        void AddCarousel(Carousel carousel);
        void DeleteCarousel(string carouselid);
        void UpdateCarousel(Carousel carousel);
        void ActivateCarousel(string crouselid);
        void CancelCarousel(string carouselid);
        Carousel GetCarouselById(string carouselid);
        List<Carousel> GetAllCarousels();


        // function for category
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(string categoryid);
        Category GetCategoryById(string categoryid);
        List<Category> GetAllCategories();

        // function for product
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(string productid);
        Product GetProductById(string productid);
        List<Product> GetAllProducts();
        List<Product> GetProductsByCategoryId(string categoryid);
        List<Product> GetPriceBySize(string productname);

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
        void DeleteCartDetail(string cartdetailid);
        CartDetail GetCartDetailByCustomerId(string customerid);
        CartDetail GetCartDetailByCartDetailId(string cartdetailid);
        List<CartDetail> GetAllCartDetails();

        // function for favorite
        void AddFavorite(Favorite favorite);
        void DeleteFavorite(string favoriteid);
        List<Favorite> GetAllFavorites();
        Favorite GetFavoriteById(string favoriteid);
        List<Favorite> GetFavoritesByCustomerId(string customerid);

        // function for order
        void AddOrder(OrderDetail orderdetailid);
        void UpdateOrder(Order order);
        void DeleteOrder(string orderid);
        Order GetOrderById(string orderid);
        List<Order> GetOrderByCustomerId(string customerid);
        List<Order> GetAllOrders();

        // function for order detail
        void AddOrderDetail(OrderDetail orderDetail);
        void UpdateOrderDetail(OrderDetail orderDetail);
        void DeleteOrderDetail(string orderdetailid);
        OrderDetail GetOrderDetailById(string orderdetailid);
        List<OrderDetail> GetOrderDetailByOrderId(string orderid);

        OrderDetail GetOrderDetailByCustomerId(string customerid);
        List<OrderDetail> GetAllOrderDetails();

        // function for comment
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(string commentid);
        Comment GetCommentById(string commentid);
        List<Comment> GetAllComments();

        void PublishComment(string commentid);

        // function confirm order
        void ConfirmOrder(string orderid, string staffid);

        // function cancel order
        void CancelOrder(string orderid);

        // function for bill
        void AddBill(Bill bill);
        void UpdateBill(Bill bill);
        void DeleteBill(string billid);
        Bill GetBillById(string bill);
        List<Bill> GetAllBills();
        List<Bill> GetBillByOrderId(string orderid);

        // function frint bill
        void PrintBill(string orderid, string staffid);

        // function for daily revenue
        int GetDailyRevenue(DateTime date);

        // function for top products
        List<TopProduct> GetTopProducts(DateTime date);
    }
}
