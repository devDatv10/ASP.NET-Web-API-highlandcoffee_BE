using System.ComponentModel.DataAnnotations;

namespace highlandcoffeeapp_BE.Models
{
    // Models for Account
    public class Account
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public string personid { get; set; }
        public int status { get; set; }
    }
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
        [Key]
        public string categoryid { get; set; }
        public string categoryname { get; set; }
        public string description { get; set; }
    }


    // Models for Customer

    public class Customer
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public string address { get; set; }
        public int point { get; set; }
        public string password { get; set; }
    }


    // Models for Staff
    public class Staff
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public DateTime startday { get; set; }
        public int salary { get; set; }
        public string password { get; set; }
    }


    // Models for Product
    public class Product
    {
        public string productid { get; set; }
        public string categoryid { get; set; }
        public string productname { get; set; }
        public string description { get; set; }
        public string size { get; set; }
        public int price { get; set; }
        public string unit { get; set; }
        public byte[] image { get; set; }
        public byte[] imagedetail { get; set; }
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

    // Models for Food
    public class Food
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
}
