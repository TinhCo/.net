namespace backend.Models;
public partial class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public string Summary { get; set; } = null!;
    public string? Description { get; set; }
    public string? Quote { get; set; }
    public string? Photo { get; set; }
    public string? Tags { get; set; }
    public long? PostCatId { get; set; }
    public long? PostTagId { get; set; }
    public long? AddedBy { get; set; }
    public string Status { get; set; } = null!;
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set;}
    public virtual User? AddedByNavigation { get; set; }
    public virtual PostCategory? PostCat { get; set; }
    public virtual ICollection<PostComment> PostComments { get; } = new List<PostComment>();
    public virtual PostTag? PostTag { get; set; }
}
