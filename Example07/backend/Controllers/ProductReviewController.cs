
using backend.Context;
using backend.FormInput;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : Controller
    {
        private readonly Example07Context _context;
        public ProductReviewController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<ProductReview>>> Get()
        {
            if (_context.ProductReviews == null)
            {
                return NotFound();
            }
            return await _context.ProductReviews.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductReview>>> Get(long id)
        {
            if (_context.ProductReviews == null)
            {
                return NotFound();
            }
            var product = await _context.ProductReviews.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.ProductReviews == null)
            {
                return NotFound();
            }
            var product = await _context.ProductReviews.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.ProductReviews.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormProductReviewView value)
        {
            try
            {
                var n = new ProductReview()
                {
                    CreatedAt = DateTime.Now,
                    ProductId = value.ProductId,
                    UserId = value.UserId,
                    Rate = value.Rate,
                    Review = value.Review,
                    Status = value.Status,
                    User = await _context.Users.FindAsync(value.UserId),
                    Product = await _context.Products.FindAsync(value.ProductId),
                };
                _context.ProductReviews.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormProductReviewView value)

        {
            var find = await _context.ProductReviews.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.ProductId = value.ProductId;
            find.UserId = value.UserId;
            find.Rate = value.Rate;

            find.Review = value.Review;
            find.Status = value.Status;
            find.User = await _context.Users.FindAsync(value.UserId);
            find.Product = await _context.Products.FindAsync(value.ProductId);
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
