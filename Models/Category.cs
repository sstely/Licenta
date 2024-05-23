using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Models
{
    public class Category
    {

        public int ID { get; set; }

        [Display(Name = "Categorie")]
        public string CategoryName { get; set; }

        [Display(Name = "Marja")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal CategoryPrice { get; set; }

        public ICollection<Dish>? Dishes { get; set; }

        public string? CategoryImageURL { get; set; }


        [NotMapped]

        public IFormFile? CategoryImageFile { get; set; }
    }
}
