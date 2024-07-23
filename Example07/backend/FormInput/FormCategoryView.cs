namespace backend.FormInput
{
    public class FormCategoryView
    {
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string? Summary { get; set; }
        public string? Photo { get; set; }
        public short IsParent { get; set; }
        public long? ParentId { get; set; }
        public long? AddedBy { get; set; }
        public string Status { get; set; } = null!;
    }
}
