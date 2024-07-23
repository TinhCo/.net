namespace backend.Models
{
    public partial class Category
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Photo { get; set; }
        public short IsParent { get; set; }
        public long? ParentId { get; set; }
        public long? AddedBy { get; set;}
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get;set; }
        public virtual User? AddedByNavigation { get; set; }
        public virtual ICollection<Category> InverseParent { get; } = new List<Category>();
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
