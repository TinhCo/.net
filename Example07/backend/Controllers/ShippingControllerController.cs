using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System;
using System.Threading.Tasks;
using backend.Context;
using backend.FormInput;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingController : ControllerBase
    {
        private readonly Example07Context _context;
        public ShippingController(Example07Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipping>>> Get()
        {
            if (_context.Shippings == null)
            {
                return NotFound();
            }
            return await _context.Shippings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Shipping>>> Get(long id)
        {
            if (_context.Shippings == null)
            {
                return NotFound();
            }
            var product = await _context.Shippings.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Shippings == null)
            {
                return NotFound();
            }
            var product = await _context.Shippings.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Shippings.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // POST: api/Shipping
        [HttpPost]
        public async Task<IActionResult> PostShipping([FromBody] FormShippingView shippingFormView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipping = new Shipping
            {
                Type = shippingFormView.Type,
                Price = shippingFormView.Price,
                Status = shippingFormView.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Shippings.Add(shipping);
            await _context.SaveChangesAsync();

            return Ok(shipping);
        }

        // PUT: api/Shipping/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipping(long id, [FromBody] FormShippingView shippingFormView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shipping = await _context.Shippings.FindAsync(id);

            if (shipping == null)
            {
                return NotFound();
            }

            shipping.Type = shippingFormView.Type;
            shipping.Price = shippingFormView.Price;
            shipping.Status = shippingFormView.Status;
            shipping.UpdatedAt = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(shipping);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
