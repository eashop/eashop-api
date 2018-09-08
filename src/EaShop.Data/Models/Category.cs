using System.ComponentModel.DataAnnotations;

namespace EaShop.Data.Models
{
    class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int? ParentId { get; set; }
    }
}
