using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EaShop.Data.Models
{
    class GoodsInOrder
    {
        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
