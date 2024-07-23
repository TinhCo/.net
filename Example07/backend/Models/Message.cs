namespace backend.Models;

    public partial class Message
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; } = null!;
        public string Message1 { get; set; } = null!;
        public DateTime? ReadAt { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set;}
    }

