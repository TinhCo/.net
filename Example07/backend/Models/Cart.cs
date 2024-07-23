namespace backend.Models;
public partial class Cart
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public long? OrderId { get; set; }
    public long? UserId { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Amount { set; get; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;set; }
    public virtual Order? Order { get; set; }
    public virtual Product Product { get; set; } = null!;
    public virtual User? User { get; set; }
    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();
    

}
