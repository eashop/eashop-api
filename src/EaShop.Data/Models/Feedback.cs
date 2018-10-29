using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EaShop.Data.Models
{
    public class Feedback
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int GoodsId { get; set; }

        [JsonIgnore]
        public ApplicationUser User { get; set; }

        [JsonIgnore]
        public Goods Goods { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public double Rating { get; set; }
    }
}