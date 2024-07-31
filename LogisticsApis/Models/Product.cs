using System.ComponentModel.DataAnnotations;

namespace LogisticsApis.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }

        public string? Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int WarehouseId { get; set; }
        public int? Rating { get; set; }
        public Warehouse Warehouse { get; set; }
 
    }
}
