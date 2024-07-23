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
    public class CategoryController : Controller
    {
        private readonly Example07Context _context;
        public CategoryController(Example07Context context)
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
        public async Task<ActionResult<IEnumerable<Category>>> Get()
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var categories = _context.Categories
              .Select(value => new Category
              {
                  Id = value.Id,
                  Title = value.Title,
                  Slug = value.Slug,
                  Summary = value.Summary,
                  Photo = value.Photo,
                  IsParent = value.IsParent,
                  ParentId = value.ParentId,
                  CreatedAt = value.CreatedAt,
                  UpdatedAt = value.UpdatedAt,
                  AddedBy = value.AddedBy,
                  Status = value.Status,
                  Parent = value.ParentId.HasValue ? _context.Categories.Where(a => a.Id == value.ParentId.Value)
                                                        .Select(value => new Category
                                                        {
                                                            Id = value.Id,
                                                            Title = value.Title,
                                                            Slug = value.Slug,
                                                            Summary = value.Summary,
                                                            Photo = value.Photo,


                                                            CreatedAt = value.CreatedAt,
                                                            UpdatedAt = value.UpdatedAt,
                                                            AddedBy = value.AddedBy,
                                                            Status = value.Status,
                                                        }).FirstOrDefault() : null,
                  AddedByNavigation = _context.Users.Where(a => a.Id == value.AddedBy.Value)
                                                      .Select(value => new User
                                                      {
                                                          Id = value.Id,
                                                          Name = value.Name,
                                                          Email = value.Email,
                                                          Password = value.Password,
                                                          Photo = value.Photo,
                                                          Role = value.Role,
                                                          Provider = value.Provider,
                                                          ProviderId = value.ProviderId,
                                                          Status = value.Status,
                                                          CreatedAt = value.CreatedAt,
                                                          UpdatedAt = value.UpdatedAt,
                                                      }).First(),
              })
              .ToList();




            return categories;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Category>>> Get(long id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var categories = _context.Categories
              .Select(value => new Category
              {
                  Id = value.Id,
                  Title = value.Title,
                  Slug = value.Slug,
                  Summary = value.Summary,
                  Photo = value.Photo,
                  IsParent = value.IsParent,
                  ParentId = value.ParentId,
                  CreatedAt = value.CreatedAt,
                  UpdatedAt = value.UpdatedAt,
                  AddedBy = value.AddedBy,
                  Status = value.Status,
                  Parent = value.ParentId.HasValue ? _context.Categories.Where(a => a.Id == value.ParentId.Value)
                                                        .Select(value => new Category
                                                        {
                                                            Id = value.Id,
                                                            Title = value.Title,
                                                            Slug = value.Slug,
                                                            Summary = value.Summary,
                                                            Photo = value.Photo,


                                                            CreatedAt = value.CreatedAt,
                                                            UpdatedAt = value.UpdatedAt,
                                                            AddedBy = value.AddedBy,
                                                            Status = value.Status,
                                                        }).FirstOrDefault() : null,
                  AddedByNavigation = _context.Users.Where(a => a.Id == value.AddedBy.Value)
                                                      .Select(value => new User
                                                      {
                                                          Id = value.Id,
                                                          Name = value.Name,
                                                          Email = value.Email,
                                                          Password = value.Password,
                                                          Photo = value.Photo,
                                                          Role = value.Role,
                                                          Provider = value.Provider,
                                                          ProviderId = value.ProviderId,
                                                          Status = value.Status,
                                                          CreatedAt = value.CreatedAt,
                                                          UpdatedAt = value.UpdatedAt,
                                                      }).First(),
              }).Where(e => e.Id == id).FirstOrDefault();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FormCategoryView value)
        {
            try
            {
                var n = new Category()
                {
                    CreatedAt = DateTime.Now,
                    Title = value.Title,
                    Slug = GenerateSlug(value.Title),
                    Summary = value.Summary,
                    Photo = value.Photo,
                    IsParent = value.IsParent,

                    AddedBy = value.AddedBy,
                    AddedByNavigation = await _context.Users.FindAsync(value.AddedBy),
                    Status = value.Status,
                };
                _context.Categories.Add(n);
                if (n.IsParent == 1)
                {
                    n.ParentId = value.ParentId;
                    n.Parent = await _context.Categories.FindAsync(value.ParentId);

                }
                _context.Categories.Add(n);
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
        public async Task<IActionResult> Put(long id, [FromBody] FormCategoryView value)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                // Cập nhật các thông tin của danh mục
                category.Title = value.Title;
                category.Summary = value.Summary;
                category.Photo = value.Photo;
                category.IsParent = value.IsParent;
                category.Status = value.Status;


                if (value.IsParent == 1)
                {
                    category.ParentId = value.ParentId;
                    category.Parent = await _context.Categories.FindAsync(value.ParentId);
                }
                else
                {

                    category.ParentId = null;
                    category.Parent = null;
                }
                if (value.AddedBy != category.AddedBy && value.AddedBy != null)
                {
                    category.AddedBy = value.AddedBy;
                    category.AddedByNavigation = await _context.Users.FindAsync(value.AddedBy);
                }
                category.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return StatusCode(200, "Cập nhật thành công");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Cập nhật danh mục thất bại: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var product = await _context.Categories.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}