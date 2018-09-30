using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EaShop.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }

        [JsonIgnore]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}