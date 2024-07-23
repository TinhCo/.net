
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
    public class PostController : Controller
    {
        private readonly Example07Context _context;
        public PostController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            return await _context.Posts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Post>>> Get(long id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var product = await _context.Posts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Posts == null)
            {
                return NotFound();
            }
            var product = await _context.Posts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormPostView value)
        {
            try
            {
                var n = new Post()
                {
                    CreatedAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Status = value.Status,
                    Summary = value.Summary,
                    Description = value.Description,
                    Quote = value.Quote,
                    Photo = value.Photo,
                    Tags = value.Tags,
                    PostCatId = value.PostCatId,
                    PostTagId = value.PostTagId,
                    AddedBy = value.AddedBy,
                    PostCat = await _context.PostCategories.FindAsync(value.PostCatId),
                    PostTag = await _context.PostTags.FindAsync(value.PostTagId),
                    AddedByNavigation = await _context.Users.FindAsync(value.AddedBy),
                };

                _context.Posts.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormPostView s)
        {
            var find = await _context.Posts.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Title = s.Title;
            find.Slug = GenerateSlug(s.Title);
            find.Status = s.Status;
            find.Summary = s.Summary;
            find.Description = s.Description;
            find.Quote = s.Quote;
            find.Photo = s.Photo;
            find.Tags = s.Tags;
            find.PostCatId = s.PostCatId;
            find.PostTagId = s.PostTagId;
            find.AddedBy = s.AddedBy;


            find.PostCat = await _context.PostCategories.FindAsync(s.PostCatId);
            find.PostTag = await _context.PostTags.FindAsync(s.PostTagId);
            find.AddedByNavigation = await _context.Users.FindAsync(s.AddedBy);

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
