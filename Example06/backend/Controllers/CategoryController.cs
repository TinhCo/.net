using backend.Context;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly ProductContext db;
        public CategoryController(ProductContext db) 
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Category> Get() 
        {
            return db.Categories.ToList();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormCategoryView category)
        {
            var cate = new Category()
            {
                Name = category.Name,
                SlugCategory = category.SlugCategory
            };
            db.Categories.Add(cate);
            await db.SaveChangesAsync();
            if (cate.idCategory > 0)
            { 
                return Ok(1); 
            }
            return Ok(0);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) 
        { }

        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}
