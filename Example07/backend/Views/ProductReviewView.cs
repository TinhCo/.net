using backend.Models;

namespace backend.Views
{
    public class ProductReviewView
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? ProductId { get; set; }
        public short Rate { get; set; }
        public string? Review { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
    public class ProductReviewProductView
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public short Rate { get; set; }
        public string? Review { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual User? User { get; set; }
    }
}
