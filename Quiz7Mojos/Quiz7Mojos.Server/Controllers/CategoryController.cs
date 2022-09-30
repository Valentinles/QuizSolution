using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quiz7Mojos.Server.Models;
using Quiz7Mojos.Server.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz7Mojos.Server.Controllers
{
    public class CategoryController : ApiController
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Route(nameof(GetCategories))]
        public IActionResult GetCategories()
        {
            var categories = this.categoryService.GetCategories();

            var response = new List<CategoryResponseModel>();
            foreach (var category in categories)
            {
                var responseModel = new CategoryResponseModel()
                {
                    Category = category
                };
                response.Add(responseModel);
            }

            return Ok(response);
        }
    }
}
