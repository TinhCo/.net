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
    public class MessageController : Controller
    {
        private readonly Example07Context _context;
        public MessageController(Example07Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> Get()
        {
            if (_context.Messages == null)
            {
                return NotFound();
            }
            return await _context.Messages.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Message>>> Get(long id)
        {
            if (_context.Messages == null)
            {
                return NotFound();
            }
            var product = await _context.Messages.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Messages == null)
            {
                return NotFound();
            }
            var product = await _context.Messages.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Messages.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormMessageView value)
        {
            try
            {
                var n = new Message()
                {
                    CreateAt = DateTime.Now,
                    Name = value.Name,
                    Subject = value.Subject,
                    Email = value.Email,
                    Message1 = value.Message1,

                };

                _context.Messages.Add(n);
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
        public async Task<IActionResult> Put(int id, [FromBody] FormMessageView value)
        {
            var find = await _context.Messages.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Name = value.Name;
            find.Subject = value.Subject;
            find.Email = value.Email;
            find.Message1 = value.Message1;
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

    }
}
