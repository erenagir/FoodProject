using Microsoft.AspNetCore.Http;

namespace ProductProject.Data
{
    public class ProductImage
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile ImageURL { get; set; }
        public string ImageName { get; set; }
        public int Stock { get; set; }
        public int CategoryID { get; set; }
    }
}
