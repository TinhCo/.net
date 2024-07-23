namespace backend.Models
{
    public partial class Wishlist
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long? CartId { get; set; }
        public long? UserId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Cart? Cart { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}