namespace backend.Models;

public partial class PostTag
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Status { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;set; }
    public virtual ICollection<Post> Posts { get;} = new List<Post>();
}
