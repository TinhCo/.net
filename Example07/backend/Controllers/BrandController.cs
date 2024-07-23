using backend.Context;
using backend.FormInput;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : Controller
    {
        private readonly Example07Context _context;
        public BrandController(Example07Context context)
        {
            _context = context;
        }
        private string GenerateSlug(string title)
        {

            var slug = title.ToLower().Trim();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            return slug;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> Get()
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            return await _context.Brands.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Brand>>> Get(long id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var product = await _context.Brands.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormBrandView value)
        {
            try
            {
                var n = new Brand()
                {
                    CreatedAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Status = value.Status,

                };

                _context.Brands.Add(n);
                await _context.SaveChangesAsync();
                try
                {

                    return StatusCode(200);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Thêm danh mục thất bại: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] FormBrandView s)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            brand.Title = s.Title;
            brand.Slug = GenerateSlug(s.Title);
            brand.Status = s.Status;

            brand.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            try
            {
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Cập nhật danh mục thất bại: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var product = await _context.Brands.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Brands.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_context.Brands?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
