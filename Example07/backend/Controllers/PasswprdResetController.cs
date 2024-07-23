
using backend.Context;
using backend.FormInput;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswprdResetController : Controller
    {
        private readonly Example07Context _context;
        public PasswprdResetController(Example07Context context)
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
        public static string GenerateToken(int length = 32)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenBytes = new byte[length];
                rng.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormPasswordResetView value)
        {
            try
            {
                var n = new PasswordReset()
                {
                    CreatedAt = DateTime.Now,
                    Email = value.Email,
                    Token = GenerateToken(),

                };

                _context.PasswordResets.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormPasswordResetView value)

        {
            var find = await _context.PasswordResets.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Email = value.Email;
            find.Token = GenerateToken();
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
