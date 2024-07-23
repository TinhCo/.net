namespace backend.Models;
    public partial class Notification
    {
        public string Id { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string NotifiableType { get; set; } = null!;
        public long NotifiableId { get; set; }
        public string Data { get; set; } = null!;
        public DateTime? ReadAt { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set;}
    }

