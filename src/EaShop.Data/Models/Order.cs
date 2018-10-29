using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EaShop.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [JsonIgnore]
        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ShipmentMethod { get; set; }

        [JsonIgnore]
        public virtual ICollection<GoodsInOrder> GoodsInOrder { get; set; }
    }
}