using System.ComponentModel.DataAnnotations.Schema;


namespace EaShop.Data.Models
{
    class Feedback
    {
        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public double Rating { get; set; }
    }
}
