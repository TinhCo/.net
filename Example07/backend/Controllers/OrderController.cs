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
    public class OrderController : ControllerBase
    {
        private readonly Example07Context _context;
        public OrderController(Example07Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var orders = _context.Orders.Select(value => new Order
            {
                Id = value.Id,
                OrderNumber = value.OrderNumber,
                UserId = value.UserId,
                ShippingId = value.ShippingId,
                Coupon = value.Coupon,
                PaymentMethod = value.PaymentMethod,
                PaymentStatus = value.PaymentStatus,
                Status = value.Status,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                Phone = value.Phone,
                Country = value.Country,
                PostCode = value.PostCode,
                Address1 = value.Address1,
                Address2 = value.Address2,
                SubTotal = value.SubTotal,
                TotalAmount = value.TotalAmount, // Tính toán TotalAmount từ SubTotal và Coupon
                Quantity = value.Quantity,
                CreatedAt = value.CreatedAt,
                UpdatedAt = value.UpdatedAt,
                //User = _context.Users.Where(a => a.Id == value.UserId)
                //                    .Select(value => new User
                //                    {
                //                        Id = value.Id,
                //                        Name = value.Name,
                //                        Email = value.Email,
                //                        Password = value.Password,
                //                        Photo = value.Photo,
                //                        Role = value.Role,
                //                        Provider = value.Provider,
                //                        ProviderId = value.ProviderId,
                //                        Status = value.Status,
                //                        CreatedAt = value.CreatedAt,
                //                        UpdatedAt = value.UpdatedAt,
                //                    }).First(),
                User = new User
                {
                    Id = value.User.Id,
                    Name = value.User.Name,
                    Email = value.User.Email,
                    Password = value.User.Password,
                    Photo = value.User.Photo,
                    Role = value.User.Role,
                    Provider = value.User.Provider,
                    ProviderId = value.User.ProviderId,
                    Status = value.User.Status,
                    CreatedAt = value.User.CreatedAt,
                    UpdatedAt = value.User.UpdatedAt,
                },
                //Shipping = _context.Shippings.Where(a => a.Id == value.ShippingId)
                //                                .Select(value => new Shipping
                //                                {
                //                                    Type = value.Type,
                //                                    Price = value.Price,
                //                                    Status = value.Status,
                //                                    CreatedAt = value.CreatedAt,
                //                                    UpdatedAt = value.UpdatedAt,
                //                                }).First(),
                Shipping = new Shipping
                {
                    Type = value.Shipping.Type,
                    Price = value.Shipping.Price,
                    Status = value.Shipping.Status,
                    CreatedAt = value.Shipping.CreatedAt,
                    UpdatedAt = value.Shipping.UpdatedAt,
                }
            }).ToList();
            return orders;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> Get(long id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var product = await _context.Orders.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            var product = await _context.Orders.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] FormOrderView orderFormView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            decimal subTotal = 0;
            int quantity = 0;
            foreach (var cartItem in orderFormView.Carts)
            {
                subTotal += cartItem.Price * cartItem.Quantity;
                quantity += cartItem.Quantity;
            }

            // Tạo đối tượng Order
            var order = new Order
            {
                OrderNumber = orderFormView.OrderNumber,
                UserId = orderFormView.UserId,
                ShippingId = orderFormView.ShippingId,
                Coupon = orderFormView.Coupon,
                PaymentMethod = orderFormView.PaymentMethod,
                PaymentStatus = orderFormView.PaymentStatus,
                Status = orderFormView.Status,
                FirstName = orderFormView.FirstName,
                LastName = orderFormView.LastName,
                Email = orderFormView.Email,
                Phone = orderFormView.Phone,
                Country = orderFormView.Country,
                PostCode = orderFormView.PostCode,
                Address1 = orderFormView.Address1,
                Address2 = orderFormView.Address2,
                SubTotal = subTotal,
                TotalAmount = subTotal - (orderFormView.Coupon ?? 0), // Tính toán TotalAmount từ SubTotal và Coupon
                Quantity = quantity,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                User = await _context.Users.FindAsync(orderFormView.UserId),
                Shipping = await _context.Shippings.FindAsync(orderFormView.ShippingId),
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            long orderId = order.Id;
            long? userId = order.UserId;

            foreach (var cartItem in orderFormView.Carts)
            {
                var cart = new Cart
                {
                    CreatedAt = DateTime.Now,
                    ProductId = cartItem.ProductId,
                    UserId = userId,
                    Price = cartItem.Price,
                    Status = cartItem.Status,
                    Quantity = cartItem.Quantity,
                    Amount = cartItem.Price * cartItem.Quantity,
                    User = await _context.Users.FindAsync(userId),
                    Product = await _context.Products.FindAsync(cartItem.ProductId),
                    OrderId = orderId
                };

                _context.Carts.Add(cart);
            }


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

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(long id, [FromBody] FormOrderView orderFormView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            order.OrderNumber = orderFormView.OrderNumber;
            order.UserId = orderFormView.UserId;
            order.ShippingId = orderFormView.ShippingId;
            order.Coupon = orderFormView.Coupon;
            order.PaymentMethod = orderFormView.PaymentMethod;
            order.PaymentStatus = orderFormView.PaymentStatus;
            order.Status = orderFormView.Status;
            order.FirstName = orderFormView.FirstName;
            order.LastName = orderFormView.LastName;
            order.Email = orderFormView.Email;
            order.Phone = orderFormView.Phone;
            order.Country = orderFormView.Country;
            order.PostCode = orderFormView.PostCode;
            order.Address1 = orderFormView.Address1;
            order.Address2 = orderFormView.Address2;
            order.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            try
            {
                return StatusCode(200, "Cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Cập nhật thất bại: {ex.Message}");
            }
        }
    }
}
