using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{

        [Table("Product")]
        public class Product
        {
            [Key]
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string imageUrl { get; set; }
            public string price { get; set; }

        }
}
