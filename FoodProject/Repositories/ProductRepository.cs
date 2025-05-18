using Microsoft.EntityFrameworkCore;
using ProductProject.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductProject.Repositories
{
    public class ProductRepository : GenericRepository<Product> // T'yi karşılayan yapı Product olacak
    {
        public ProductRepository(Context context) : base(context)
        {
        }
        public List<Product> GetByCategoryId(int categoryId)
        {
            return _context.Products.Where(p => p.CategoryID == categoryId).ToList();
        }
    }
}
