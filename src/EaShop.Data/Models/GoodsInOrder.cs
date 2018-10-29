using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace EaShop.Data.Models
{
    public class GoodsInOrder
    {
        [Required]
        public int GoodsId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [JsonIgnore]
        public Goods Goods { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}