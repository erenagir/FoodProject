using ProductProject.Data.Models;

namespace ProductProject.Data
{
    public class ShoppingViewModel
    {
        public int ProductID { get; set; }      
        public int AppUserID { get; set; }
        public double ProductPrice { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


    }
}
