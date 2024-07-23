using backend.Models;
using backend.Context;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductContext _productContext;
        public ProductController(ProductContext productContext)
        {
            _productContext = productContext;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productContext.Products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _productContext.Products.FirstOrDefault(s => s.id == id);
        }

        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _productContext.Products.Add(value);
            _productContext.SaveChanges();
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            var product = _productContext.Products.FirstOrDefault(s => s.id == id);
            if (product != null)
            {
                _productContext.Entry<Product>(product).CurrentValues.SetValues(value);
                _productContext.SaveChanges();
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = _productContext.Products.FirstOrDefault(s => s.id == id);
            if (student != null)
            {
                _productContext.Products.Remove(student);
                _productContext.SaveChanges();
            }
        }
    }
}