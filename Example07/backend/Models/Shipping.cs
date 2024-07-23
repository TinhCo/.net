namespace backend.Models;

public partial class Shipping
{
    public long Id { get; set; }
    public string Type { get; set; } = null!;
    public decimal Price { get; set; }
    public string Status { get; set; } = null!; 
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get;set; }
    public virtual ICollection<Order> Orders { get;} = new List<Order>();
}
