using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Data.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int ProductQuantity { get; set; }
        public DateTime ProductOrderDate { get; set; }
        public int ProductStock { get; set; }
        public int AppUserID { get; set; }
        // public virtual AppUser AppUser { get; set; }
        [NotMapped]
        public List<AppUser> AppUsers { get; set; }

    }
}
