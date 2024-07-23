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
    public class UserController : Controller
    {
        private readonly Example07Context _context;
        public UserController(Example07Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> Get(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var product = await _context.Users.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var product = await _context.Users.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Users.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormUserView value)
        {
            try
            {
                var n = new User()
                {
                    CreatedAt = DateTime.Now,
                    Name = value.Name,
                    Email = value.Email,
                    Password = value.Password,
                    Photo = value.Photo,
                    Role = value.Role,
                    Provider = value.Provider,
                    ProviderId = value.ProviderId,
                    Status = value.Status,

                };

                _context.Users.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormUserView value)
        {
            var find = await _context.Users.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }


            find.Name = value.Name;
            find.Email = value.Email;
            find.Password = value.Password;
            find.Photo = value.Photo;
            find.Role = value.Role;
            find.Provider = value.Provider;
            find.ProviderId = value.ProviderId;
            find.Status = value.Status;
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
