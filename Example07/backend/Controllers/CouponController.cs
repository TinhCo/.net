﻿
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
    public class CouponController : Controller
    {
        private readonly Example07Context _context;
        public CouponController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Coupon>>> Get()
        {
            if (_context.Coupons == null)
            {
                return NotFound();
            }
            return await _context.Coupons.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Coupon>>> Get(long id)
        {
            if (_context.Coupons == null)
            {
                return NotFound();
            }
            var product = await _context.Coupons.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormCouponView value)
        {
            try
            {
                var n = new Coupon()
                {
                    CreateAt = DateTime.Now,
                    Code = value.Code,
                    Type = value.Type,
                    Value = value.Value,
                    Status = value.Status,
                };

                _context.Coupons.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormCouponView value)

        {
            var find = await _context.Coupons.FindAsync(id);
            if (find == null)
            {
                return NotFound();
            }

            find.Code = value.Code;
            find.Type = value.Type;
            find.Value = value.Value;
            find.Status = value.Status;
            find.UpdateAt = DateTime.Now;

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
            if (_context.Coupons == null)
            {
                return NotFound();
            }
            var product = await _context.Coupons.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Coupons.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
