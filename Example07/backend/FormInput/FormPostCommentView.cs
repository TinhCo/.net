namespace backend.FormInput
{
    public class FormPostCommentView
    {
        public long? UserId { get; set; }
        public long? PostId { get; set; }
        public string Comment { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? RepliedComment { get; set; }
        public long? ParentId { get; set; }
    }
}
