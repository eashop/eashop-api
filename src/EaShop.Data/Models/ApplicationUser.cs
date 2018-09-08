using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EaShop.Data.Models
{
    public class ApplicationUser: IdentityUser, IApplicationDbContext
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [StringLength(255)]
        public string Address { get; set; }
    }
}