using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProductProject.Data.Models
{
    public class About
    {
        [Key]
        public int AboutID { get; set; }
        public string AboutTitle { get; set; }
        public string AboutText { get; set; }
        public string AboutImageURL { get; set; }
    }
}
