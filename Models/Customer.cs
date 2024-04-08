namespace highlandcoffeeapp_BE.Models
{
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
}
