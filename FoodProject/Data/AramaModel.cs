using ProductProject.Data.Models;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;

namespace ProductProject.Data
{
    public class AramaModel
    {
        public string AramaKey { get; set; }
        public List<Product> Products { get; set; }
        public List<About> Abouts { get; set; }
    }
}
