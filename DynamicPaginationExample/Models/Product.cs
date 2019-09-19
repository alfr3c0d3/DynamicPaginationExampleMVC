using System.ComponentModel.DataAnnotations;

namespace DynamicPaginationExample.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
