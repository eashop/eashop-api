using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EaShop.Data.Models
{
    public class Goods
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [StringLength(10)]
        public string Size { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        [JsonIgnore]
        public virtual ICollection<Feedback> Feedbacks { get; set; }

        [JsonIgnore]
        public virtual ICollection<GoodsInOrder> GoodsInOrder { get; set; }


    }
}
