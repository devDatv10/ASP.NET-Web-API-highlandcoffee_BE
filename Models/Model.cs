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

        public string name { get; set; }

        public string email { get; set;}

        public string password { get; set; }

        public string confirm_password { get; set; }

        public byte[] image { get; set; }

        public string address { get; set;}

        public int phone_number { get; set;}
    }

    // Models for Staff
    public class Staff
    {
        public int id { get; set; }
        public string staff_name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int phone_number { get; set; }
        public string address { get; set; }
    }

    // Models for Product
    public class Product
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Order
    public class Order
    {
        public int id { get; set; }
    }

    // Models for OrderDetail
    public class OrderDetail
    {
        public int id { get; set; }
    }

    // Models for Cart
    public class Cart
    {
        public int id { get; set; }
    }

    // Models for CartDetail
    public class CartDetail
    {
        public int id { get; set; }
    }

    // Models for Other
    public class Other
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Coffee
    public class Coffee
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Tea
    public class Tea
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Bread
    public class Bread
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Cake
    public class Cake
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Freeze
    public class Freeze
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Popular
    public class Popular
    {
        public int id { get; set; }

        public int category_id { get; set; }
        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    // Models for Favorite
    public class Favorite
    {
        public int id { get; set; }

        public int category_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int new_price { get; set; }

        public int old_price { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }

        public int quantity { get; set; }
    }

    public class Test{
        public int id { get; set; }

        public byte[] image_path { get; set; }
    }
}
