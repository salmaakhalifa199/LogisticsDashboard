using System.ComponentModel.DataAnnotations;

namespace LogisticsApis.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
