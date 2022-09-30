using System.Collections.Generic;

namespace Quiz7Mojos.Server.Services
{
    public interface ICategoryService
    {
        ICollection<string> GetCategories();
    }
}
