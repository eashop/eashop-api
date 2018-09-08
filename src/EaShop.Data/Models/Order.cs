using System;
using System.ComponentModel.DataAnnotations;

namespace EaShop.Data.Models
{
    class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

    }
}
