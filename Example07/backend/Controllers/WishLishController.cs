
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
    public class WishLishController : Controller
    {
        private readonly Example07Context _context;
        public WishLishController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Wishlist>>> Get()
        {
            if (_context.Wishlists == null)
            {
                return NotFound();
            }
            return await _context.Wishlists.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Wishlist>>> Get(long id)
        {
            if (_context.Wishlists == null)
            {
                return NotFound();
            }
            var product = await _context.Wishlists.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Wishlists == null)
            {
                return NotFound();
            }
            var product = await _context.Wishlists.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Wishlists.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormWishLishView value)
        {
            try
            {
                var n = new Wishlist()
                {
                    CreatedAt = DateTime.Now,
                    ProductId = value.ProductId,
                    UserId = value.UserId,
                    Price = value.Price,

                    Quantity = value.Quantity,
                    Amount = value.Price * value.Quantity,
                    User = await _context.Users.FindAsync(value.UserId),
                    Product = await _context.Products.FindAsync(value.ProductId),
                };

                _context.Wishlists.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormWishLishView value)

        {
            var find = await _context.Wishlists.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.ProductId = value.ProductId;
            find.UserId = value.UserId;
            find.Price = value.Price;

            find.Quantity = value.Quantity;
            find.Amount = value.Price * value.Quantity;
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
