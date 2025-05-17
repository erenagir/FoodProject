using ProductProject.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductProject.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(Context context) : base(context)
        {
        }
    }
}
