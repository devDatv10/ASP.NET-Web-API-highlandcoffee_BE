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

    // Models for Person
    public class Person
    {
        public string personid { get; set; }
        public string name { get; set; }
        public string role { get; set; }
        public string phonenumber { get; set; }
    }

    // Models for Admin
    public class Admin
    {
        public string adminid { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public int shift { get; set; }
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
        public string customerid { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public string address { get; set; }
        public int point { get; set; }
        public string password { get; set; }
        public int status { get; set; }
    }


    // Models for Staff
    public class Staff
    {
        public string staffid { get; set; }
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

    // Models for Cart
    public class Cart
    {
        public string cartid { get; set; }
        public string customerid { get; set; }
    }

    // Models for CartDetail
    public class CartDetail
    {

        public string cartdetailid { get; set; }
        public string cartid { get; set; }
        public string customerid { get; set; }
        public string productid { get; set; }
        public string productname { get; set; }
        public string size { get; set; }
        public int quantity { get; set; }
        public int totalprice { get; set; }
        public byte[] image { get; set; }
    }

    // Models for Favorite
    public class Favorite
    {
        public string favoriteid { get; set; }
        public string customerid { get; set; }
        public string productid { get; set; }
        public string productname { get; set; }
        public string description { get; set; }
        public string size { get; set; }
        public int price { get; set; }
        public string unit { get; set; }
        public byte[] image { get; set; }
        public byte[] imagedetail { get; set; }
    }

    // Models for Comment
    public class Comment
    {
        public string commentid { get; set; }
        public string customerid { get; set; }
        public string customername { get; set; }
        public string titlecomment { get; set; }
        public string contentcomment { get; set; }
        public DateTime date { get; set; }
        public byte[] image { get; set; }
        public int status { get; set; }
    }

    // Models for Order
    public class Order
    {
        public string orderid { get; set; }
        public string customerid { get; set; }
        public string staffid { get; set; }
        public DateTime date { get; set; }
        public string paymentmethod { get; set; }
        public int status { get; set; }
        public int totalprice { get; set; }
    }


    // Models for OrderDetail
    public class OrderDetail
    {
        public string orderdetailid { get; set; }
        public string orderid { get; set; }
        public string cartid { get; set; }
        public string staffid { get; set; }
        public string customerid { get; set; }
        public string productid { get; set; }
        public string productname { get; set; }
        public int quantity { get; set; }
        public string size { get; set; }
        public byte[] image { get; set; }
        public int intomoney { get; set; }
        public int totalprice { get; set; }
        public DateTime date { get; set; }
        public string paymentmethod { get; set; }
        public int status { get; set; }
        public string customername { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
    }

    public class ConfirmOrderRequest
    {
        public string orderid { get; set; }
        public string staffid { get; set; }
    }

    public class PrintBillRequest
    {
        public string orderid { get; set; }
        public string staffid { get; set; }
    }

    // Models for Bill
    public class Bill
    {
        public string billid { get; set; }
        public string orderid { get; set; }
        public string staffid { get; set; }
        public string staffname { get; set; }
        public string customerid { get; set; }
        public string customername { get; set; }
        public DateTime date { get; set; }
        public string paymentmethod { get; set; }
        public int totalprice { get; set; }
        public int discountcode { get; set; }
        public int status { get; set; }
        public string address { get; set; }
        public string phonenumber { get; set; }
    }

    public class TopProduct
    {
        public string productid { get; set; }
        public string productname { get; set; }
        public int quantitysold { get; set; }
    }
}
