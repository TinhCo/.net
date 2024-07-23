namespace backend.Models;
public partial class Product
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string? Description { get; set; }
    public string Photo {  get; set; } = null!;
    public int Stock { get; set; }
    public string? Size { get; set; }
    public string Condition { get; set; } = null!;
    public string Status { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public short IsFeatured { get; set; }
    public long? CatId { get; set; }
    public long? ChildCatId { get; set; }
    public long? BrandId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;set; }
    public virtual Brand? Brand { get; set; }
    public virtual Category? Cat {  get; set; }
    public virtual ICollection<Cart> Carts { get; } = new List<Cart>();
    public virtual ICollection<ProductReview>ProductReviews { get; } = new List<ProductReview>();
    public virtual ICollection<Wishlist> Wishlists { get; } = new List<Wishlist>();

}

