
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
    public class PostCategoryController : Controller
    {
        private readonly Example07Context _context;
        public PostCategoryController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<PostCategory>>> Get()
        {
            if (_context.PostCategories == null)
            {
                return NotFound();
            }
            return await _context.PostCategories.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PostCategory>>> Get(long id)
        {
            if (_context.PostCategories == null)
            {
                return NotFound();
            }
            var product = await _context.PostCategories.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.PostCategories == null)
            {
                return NotFound();
            }
            var product = await _context.PostCategories.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.PostCategories.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormPostCategoryView value)
        {
            try
            {
                var n = new PostCategory()
                {
                    CreatedAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Status = value.Status,

                };

                _context.PostCategories.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormPostCategoryView s)
        {
            var find = await _context.PostCategories.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Title = s.Title;
            find.Slug = GenerateSlug(s.Title);

            find.Status = s.Status;

            find.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            try
            {
                return StatusCode(200, "Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Cập nhật thất bại: {ex.Message}");
            }
        }

    }
}
