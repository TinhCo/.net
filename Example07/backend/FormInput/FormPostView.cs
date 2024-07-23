namespace backend.FormInput
{
    public class FormPostView
    {
        public string Title { get; set; }
        public string Summary { get; set; } = null!;
        public string? Description { get; set; }
        public string? Quote { get; set; }
        public string? Photo { get; set; }
        public string? Tags { get; set; }
        public long? PostCatId { get; set; }
        public long? PostTagId { get; set; }
        public long? AddedBy { get; set; }
        public string Status { get; set; } = null!;
    }
}
