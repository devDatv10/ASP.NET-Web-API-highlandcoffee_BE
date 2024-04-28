namespace highlandcoffeeapp_BE.Models
{
    // Models for Admin
    public class Admin
    {
        public string id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    // Models for Category

    public class Category
    {
        public int id { get; set; }
        public string category_name { get; set; }
        public string description { get; set; }
    }

    // Models for Customer

    public class Customer
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string confirm_password { get; set; }

        // public byte[] image { get; set; }

        public string address { get; set; }

        public int phone_number { get; set; }
        // public int point { get; set; }
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

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
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

        public int customer_id { get; set; }

        public string category_name { get; set; }

        public int product_id { get; set; }

        public int quantity { get; set; }

        public byte[] product_image { get; set; }

        public string product_name { get; set; }

        public int selected_price { get; set; }
        
        public string selected_size { get; set; }
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

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Coffee
    public class Coffee
    {
        public int id { get; set; }

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Tea
    public class Tea
    {
        public int id { get; set; }

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Bread
    public class Bread
    {
        public int id { get; set; }

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Cake
    public class Cake
    {
        public int id { get; set; }

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Freeze
    public class Freeze
    {
        public int id { get; set; }

        public string category_name { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Popular
    public class Popular
    {
        public int id { get; set; }

        public string category_name { get; set; }
        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Model for Best Sale
        public class BestSale
    {
        public int id { get; set; }

        public string category_name { get; set; }
        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }
        public int size_l_price { get; set; }
        
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    // Models for Favorite
    public class Favorite
    {
        public int id { get; set; }

        public int customer_id { get; set; }

        public string category_name { get; set; }

        public int product_id { get; set; }

        public string product_name { get; set; }

        public string description { get; set; }

        public int size_s_price { get; set; }

        public int size_m_price { get; set; }

        public int size_l_price { get; set; }
        public string unit { get; set; }

        public byte[] image { get; set; }

        public byte[] image_detail { get; set; }
    }

    public class Test
    {
        public int id { get; set; }

        public byte[] image_path { get; set; }
    }

    public class Test1
    {
        public string id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string confirm_password { get; set; }

        public string address { get; set; }

        public int phone_number { get; set; }

        public int point { get; set; }
    }
}
