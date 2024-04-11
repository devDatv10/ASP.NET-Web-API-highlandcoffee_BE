namespace highlandcoffeeapp_BE.Models
{
    // Models for Admin
    public class Admin
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
    }

    // Models for Category

    public class Category
    {
        public int id { get; set; }
        public string category_name { get; set; }
    }

    // Models for Customer

    public class Customer
    {
        public int id { get; set; }
        public string customer_name { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public string email { get; set;}
        public int phone_number { get; set;}
        public string address { get; set;}
    }

    // Models for Product
    public class Product
    {
    }

    // Models for Order
    public class Order
    {
    }

    // Models for OrderDetail
    public class OrderDetail
    {
    }

    // Models for Cart
    public class Cart
    {
    }

    // Models for CartDetail
    public class CartDetail
    {
    }

    // Models for Other
    public class Other
    {
    }

    // Models for Coffee
    public class Coffee
    {
    }

    // Models for Tea
    public class Tea
    {
    }

    // Models for Bread
    public class Bread
    {
    }

    // Models for Cake
    public class Cake
    {
    }

    // Models for Freeze
    public class Freeze
    {
    }

    // Models for Popular
    public class Popular
    {
    }

    // Models for Favorite
    public class Favorite
    {
    }

    public class Test{
        public int id { get; set; }
        public byte[] image_path { get; set; }
    }
}
