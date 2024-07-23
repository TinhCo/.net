namespace backend.Models;

public partial class PostComment
{
    public long Id { get; set; }
    public long? UserId { get; set; }
    public long? PostId { get; set; }
    public string Comment { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string? RepliedComment { get; set; }
    public long? ParentId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;set; }
    public virtual Post? Post { get; set; }
    public virtual User? User { get; set; }
}
