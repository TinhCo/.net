
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
    public class CartController : Controller
    {
        private readonly Example07Context _context;
        public CartController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Cart>>> Get()
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            return await _context.Carts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Cart>>> Get(long id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var product = await _context.Carts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormCartView value)
        {
            try
            {
                var n = new Cart()
                {
                    CreatedAt = DateTime.Now,
                    ProductId = value.ProductId,
                    //UserId = value.UserId,
                    Price = value.Price,
                    Status = value.Status,
                    Quantity = value.Quantity,
                    Amount = value.Price * value.Quantity,
                    //User = await _context.Users.FindAsync(value.UserId),
                    Product = await _context.Products.FindAsync(value.ProductId),
                };

                _context.Carts.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormCartView value)
        {
            var find = await _context.Carts.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.ProductId = value.ProductId;
            //find.UserId = value.UserId;
            find.Price = value.Price;
            find.Status = value.Status;
            find.Quantity = value.Quantity;
            find.Amount = value.Price * value.Quantity;
            //find.User = await _context.Users.FindAsync(value.UserId);
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Carts == null)
            {
                return NotFound();
            }
            var product = await _context.Carts.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Carts.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
