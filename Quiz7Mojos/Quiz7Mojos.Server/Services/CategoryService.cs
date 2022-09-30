using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quiz7Mojos.Server.Services
{
    public class CategoryService : BaseService, ICategoryService
    {

        public ICollection<string> GetCategories()
        {
            var categories = GetData().Select(x => x.Category).Distinct().ToList();

            return categories;
        }
        
    }
}
