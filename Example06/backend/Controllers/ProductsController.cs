using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Context;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ProductContext db;
        public ProductController(ProductContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            var products = db.Products.Select(s => new Product
            {
                idProduct = s.idProduct,
                Title = s.Title,
                Body = s.Body,
                Slug = s.Slug, 
                idCategory = s.idCategory,
                Category = db.Categories.Where(a => a.idCategory == s.idCategory).FirstOrDefault()
            }).ToList();
            return products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var products = db.Products.Select(s => new Product
            {
                idProduct = s.idProduct,
                Title = s.Title,
                Body = s.Body,
                Slug = s.Slug,
                idCategory=s.idCategory,
                Category = db.Categories.Where(a => a.idCategory == s.idCategory).FirstOrDefault()
            }).Where(a => a.idProduct == id).FirstOrDefault();
            return products;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormProductView _product)
        {
            var product = new Product()
            {
                Title = _product.Title,
                Body = _product.Body,
                Slug = _product.Slug,
                Category = db.Categories.Find(_product.idCategory)
            };
            db.Products.Add(product);
            await db.SaveChangesAsync();
            if (product.idProduct > 0)
            {
                return Ok(1);
            }
            return Ok(2);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FormProductView _user)
        {
            var product = db.Products.Find(id);
            product.Title = _user.Title;
            product.Body = _user.Body;
            product.Slug = _user.Slug;
            product.Category = db.Categories.Find(_user.idCategory);
            await db.SaveChangesAsync();
            return Ok(1);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return Ok(1);
        }
    }
}
