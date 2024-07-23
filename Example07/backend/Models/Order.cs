namespace backend.Models;
public partial class Order
{
    public long Id { get; set; }
    public string OrderNumber { get; set; } = null!;
    public long? UserId { get; set; }
    public decimal SubTotal { get; set; }
    public long? ShippingId { get; set; }
    public decimal? Coupon { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string PaymentStatus { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? PostCode { get; set; }
    public string Address1 { get; set; } = null!;
    public string Address2 { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();
    public virtual Shipping? Shipping { get; set; }
    public virtual User? User { get; set; }
}
