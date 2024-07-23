namespace backend.FormInput
{
    public class FormProductReviewView
    {
        public long? UserId { get; set; }
        public long? ProductId { get; set; }
        public short Rate { get; set; }
        public string? Review { get; set; }
        public string Status { get; set; } = null!;
    }
}
