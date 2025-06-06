﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductProject.Data.Models
{
	public class AppUser:IdentityUser<int>
	{
		public string NameSurname { get; set; }
        [NotMapped]
        public List<Shopping> Shoppings { get; set; }
        [NotMapped]
        public List<Payment> Payments { get; set; }
        [NotMapped]
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
