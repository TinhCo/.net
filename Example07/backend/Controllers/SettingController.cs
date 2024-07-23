
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
    public class SettingController : Controller
    {
        private readonly Example07Context _context;
        public SettingController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Setting>>> Get()
        {
            if (_context.Settings == null)
            {
                return NotFound();
            }
            return await _context.Settings.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Setting>>> Get(long id)
        {
            if (_context.Settings == null)
            {
                return NotFound();
            }
            var product = await _context.Settings.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Settings == null)
            {
                return NotFound();
            }
            var product = await _context.Settings.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Settings.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormSettingView value)
        {
            try
            {
                var n = new Setting()
                {
                    CreatedAt = DateTime.Now,
                    Description = value.Description,
                    ShortDes = value.ShortDes,
                    Logo = value.Logo,
                    Photo = value.Photo,
                    Address = value.Address,
                    Phone = value.Phone,
                    Email = value.Email,
                };
                _context.Settings.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormSettingView value)

        {
            var find = await _context.Settings.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Description = value.Description;
            find.ShortDes = value.ShortDes;
            find.Logo = value.Logo;
            find.Photo = value.Photo;
            find.Address = value.Address;
            find.Phone = value.Phone;
            find.Email = value.Email;
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
