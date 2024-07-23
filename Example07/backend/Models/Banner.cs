namespace backend.Models;
    public class Banner
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string? Photo { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }

