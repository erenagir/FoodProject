using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Data.Models
{
    public class Shopping
    {
        public int ShoppingID { get; set; }
        public int ShoppingQuantity { get; set; } = 1;
        public DateTime ShoppingDate { get; set; } = DateTime.Now;
        public double ShoppingPrice { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }
        [NotMapped]
        public List<Payment> Payments { get; set; }
    }
}
