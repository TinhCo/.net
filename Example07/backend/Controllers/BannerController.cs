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
    public class BannerController : Controller
    {
        private readonly Example07Context _context;
        public BannerController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Banner>>> Get()
        {
            if (_context.Banners == null)
            {
                return NotFound();
            }
            return await _context.Banners.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Banner>>> Get(long id)
        {
            if (_context.Banners == null)
            {
                return NotFound();
            }
            var product = await _context.Banners.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormBannerView value)
        {
            try
            {
                var n = new Banner()
                {
                    CreateAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Photo = value.Photo,
                    Description = value.Description,
                    Status = value.Status,

                };

                _context.Banners.Add(n);
                await _context.SaveChangesAsync();
                try
                {
                    return StatusCode(200, "Thêm thành công.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Thêm thất bại: {ex.Message}");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] FormBannerView s)
        {
            var find = await _context.Banners.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Title = s.Title;
            find.Slug = GenerateSlug(s.Title);
            find.Photo = s.Photo;
            find.Description = s.Description;
            find.Status = s.Status;

            find.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();

            try
            {
                return StatusCode(200, "Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Cập nhật danh mục thất bại: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Banners == null)
            {
                return NotFound();
            }
            var product = await _context.Banners.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Banners.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_context.Banners?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
