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
    public class DealsController : Controller
    {
        private readonly Example07Context _context;
        public DealsController(Example07Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Deal>>> Get()
        {
            if (_context.Deals == null)
            {
                return NotFound();
            }
            return await _context.Deals.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Deal>>> Get(long id)
        {
            if (_context.Deals == null)
            {
                return NotFound();
            }
            var product = await _context.Deals.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Deals == null)
            {
                return NotFound();
            }
            var product = await _context.Deals.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Deals.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormDealView value)
        {
            try
            {
                var n = new Deal()
                {
                    CreatedAt = DateTime.Now,
                    Sale = value.Sale,
                    Starts = value.Starts,
                    Ends = value.Ends,

                };

                _context.Deals.Add(n);
                await _context.SaveChangesAsync();
                try
                {

                    return StatusCode(200, "Thêm thành công.");
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
        public async Task<IActionResult> Put(long id, [FromBody] FormDealView value)
        {
            var find = await _context.Deals.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }


            find.Sale = value.Sale;
            find.Starts = value.Starts;
            find.Ends = value.Ends;
            find.UpdatedAt = DateTime.Now;
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

    }
}
