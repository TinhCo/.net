namespace backend.FormInput
{
    public class FormProductView
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string? Description { get; set; }
        public string Photo { get; set; } = null!;
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
    }
}
