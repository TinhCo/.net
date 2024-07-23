namespace backend.Models
{
    public class Category
    {
        public int idCategory {  get; set; }
        public string Name { get; set; }
        public string SlugCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
