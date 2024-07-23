
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
    public class PostTagController : Controller
    {
        private readonly Example07Context _context;
        public PostTagController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<PostTag>>> Get()
        {
            if (_context.PostTags == null)
            {
                return NotFound();
            }
            return await _context.PostTags.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PostTag>>> Get(long id)
        {
            if (_context.PostTags == null)
            {
                return NotFound();
            }
            var product = await _context.PostTags.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.PostTags == null)
            {
                return NotFound();
            }
            var product = await _context.PostTags.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.PostTags.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormPostTagView value)
        {
            try
            {
                var n = new PostTag()
                {
                    CreatedAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Status = value.Status,

                };

                _context.PostTags.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormPostTagView s)
        {
            var find = await _context.PostTags.FindAsync(id);
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
