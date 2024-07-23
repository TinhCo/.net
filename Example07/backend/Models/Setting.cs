namespace backend.Models;
    public partial class Setting
    {
        public long Id { get; set; }
        public string Description { get; set; } = null!;
        public string ShortDes { get; set; } = null!;
        public string Logo { get; set; } = null!;
        public string Photo {  get; set; } = null!;
        public string Address {  get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

