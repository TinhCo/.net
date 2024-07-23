using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using backend.Context;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using backend.FormInput;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Example07Context _context;
        public ProductController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var products = _context.Products.Select(value => new Product
            {
                Id = value.Id,
                Title = value.Title,
                Slug = value.Slug,
                Summary = value.Summary,
                Description = value.Description,
                Photo = value.Photo,
                Stock = value.Stock,
                Size = value.Size,
                Condition = value.Condition,
                Status = value.Status,
                Price = value.Price,
                Discount = value.Discount,
                IsFeatured = value.IsFeatured,
                CreatedAt = value.CreatedAt,
                UpdatedAt = value.UpdatedAt,
                BrandId = value.BrandId,
                CatId = value.CatId,

                Brand = _context.Brands.Where(a => a.Id == value.BrandId.Value).FirstOrDefault(),
                Cat = _context.Categories.Where(a => a.Id == value.CatId.Value).FirstOrDefault(),

            }).ToList();
            return products;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(long id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormProductView value)
        {
            try
            {
                var n = new Product()
                {
                    CreatedAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Summary = value.Summary,
                    Description = value.Description,
                    Photo = value.Photo,
                    Stock = value.Stock,
                    Size = value.Size,
                    Condition = value.Condition,
                    Status = value.Status,
                    Price = value.Price,
                    Discount = value.Discount,
                    IsFeatured = value.IsFeatured,


                };
                _context.Products.Add(n);
                if (value.BrandId != 0 && value.BrandId != null)
                {
                    n.BrandId = value.BrandId;
                    n.Brand = await _context.Brands.FindAsync(n.BrandId);
                }
                if (value.CatId != 0 && value.CatId != null)
                {
                    n.CatId = value.CatId;
                    n.Cat = await _context.Categories.FindAsync(n.CatId);
                }
                if (value.ChildCatId != 0 && value.ChildCatId != null)
                {
                    n.ChildCatId = value.ChildCatId;
                }
                _context.Products.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormProductView value)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Title = value.Title;
                product.Slug = GenerateSlug(value.Title);
                product.Summary = value.Summary;
                product.Description = value.Description;
                product.Photo = value.Photo;
                product.Stock = value.Stock;
                product.Size = value.Size;
                product.Condition = value.Condition;
                product.Status = value.Status;
                product.Price = value.Price;
                product.Discount = value.Discount;
                product.IsFeatured = value.IsFeatured;
                product.UpdatedAt = DateTime.Now;
                // Update BrandId and related Brand entity
                if (value.BrandId.HasValue && value.BrandId.Value != 0)
                {
                    product.BrandId = value.BrandId.Value;
                    product.Brand = await _context.Brands.FindAsync(value.BrandId.Value);
                }
                else
                {
                    product.BrandId = null;
                    product.Brand = null;
                }

                // Update CatId and related Category entity
                if (value.CatId.HasValue && value.CatId.Value != 0)
                {
                    product.CatId = value.CatId.Value;
                    product.Cat = await _context.Categories.FindAsync(value.CatId.Value);
                }
                else
                {
                    product.CatId = null;
                    product.Cat = null;
                }

                // Update ChildCatId
                if (value.ChildCatId.HasValue && value.ChildCatId.Value != 0)
                {
                    product.ChildCatId = value.ChildCatId.Value;
                }
                else
                {
                    product.ChildCatId = null;
                }

                await _context.SaveChangesAsync();

                return StatusCode(200, "Cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Cập nhật sản phẩm thất bại: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(long id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProductExists(long id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
