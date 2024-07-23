﻿using backend.Models;

namespace backend.Views
{
    public class BrandView
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public virtual ICollection<Product> Products { get; } = new List<Product>();
    }
}
