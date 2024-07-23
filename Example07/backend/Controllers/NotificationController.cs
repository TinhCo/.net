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
    public class NotificationController : Controller
    {
        private readonly Example07Context _context;
        public NotificationController(Example07Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> Get()
        {
            if (_context.Notifications == null)
            {
                return NotFound();
            }
            return await _context.Notifications.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Notification>>> Get(long id)
        {
            if (_context.Notifications == null)
            {
                return NotFound();
            }
            var product = await _context.Notifications.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Notifications == null)
            {
                return NotFound();
            }
            var product = await _context.Notifications.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Notifications.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormNotificationView value)
        {
            try
            {
                var n = new Notification()
                {
                    Id = value.Id,
                    CreateAt = DateTime.Now,
                    Data = value.Data,
                    Type = value.Type,
                    NotifiableType = value.NotifiableType,
                    NotifiableId = value.NotifiableId,

                };

                _context.Notifications.Add(n);
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
        public async Task<IActionResult> Put(string id, [FromBody] FormNotificationView value)
        {
            var find = await _context.Notifications.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Data = value.Data;
            find.Type = value.Type;
            find.NotifiableType = value.NotifiableType;
            find.NotifiableId = value.NotifiableId;
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
