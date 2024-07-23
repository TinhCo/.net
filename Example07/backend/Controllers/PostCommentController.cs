
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
    public class PostCommentController : Controller
    {
        private readonly Example07Context _context;
        public PostCommentController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<PostComment>>> Get()
        {
            if (_context.PostComments == null)
            {
                return NotFound();
            }
            return await _context.PostComments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PostComment>>> Get(long id)
        {
            if (_context.PostComments == null)
            {
                return NotFound();
            }
            var product = await _context.PostComments.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.PostComments == null)
            {
                return NotFound();
            }
            var product = await _context.PostComments.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.PostComments.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormPostCommentView value)
        {
            try
            {
                var n = new PostComment()
                {
                    CreatedAt = DateTime.Now,
                    PostId = value.PostId,
                    UserId = value.UserId,
                    Comment = value.Comment,
                    Status = value.Status,
                    User = await _context.Users.FindAsync(value.UserId),
                    Post = await _context.Posts.FindAsync(value.PostId),
                };
                _context.PostComments.Add(n);
                if (value.RepliedComment != null)
                {
                    n.RepliedComment = value.RepliedComment;
                }
                if (value.ParentId != null && value.ParentId != 0)
                {
                    n.ParentId = value.ParentId;
                }
                _context.PostComments.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormPostCommentView value)

        {
            var find = await _context.PostComments.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.PostId = value.PostId;
            find.UserId = value.UserId;
            find.Comment = value.Comment;
            find.Status = value.Status;
            find.User = await _context.Users.FindAsync(value.UserId);
            find.Post = await _context.Posts.FindAsync(value.PostId);
            if (value.ParentId != null && value.ParentId != 0) { find.ParentId = value.ParentId; }
            if (value.RepliedComment != null)
            {
                find.RepliedComment = value.RepliedComment;
            }
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
