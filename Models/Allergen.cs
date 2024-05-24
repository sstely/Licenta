using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Licenta.Models
{
    public class Allergen
    {
        public int ID { get; set; }

        [Display(Name = "Alergen")]
        public string AllergenName { get; set; }

        public string? AllergenImageURL { get; set; }



        public ICollection<DishAllergen>? DishAllergens { get; set; }

        [NotMapped]

        public IFormFile? AllergenImageFile { get; set; }

    }
}
