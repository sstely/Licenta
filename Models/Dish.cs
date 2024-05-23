using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Models
{
    public class Dish
    {
        public int ID { get; set; }
        
        [Display(Name = "Preparat")]
        public string Name {  get; set; }

        [Display(Name = "Descriere")]
        public string? Description { get; set; }

        [Display(Name = "Cantitate")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Amount { get; set; }

        [Display(Name = "Unitate masura")]
        public string Unit { get; set; }

        [Display(Name = "Recomandare servire")]
        public string? ServingRecommendation { get; set; }

        public string? CoverImageURL { get; set; }


        public int? CategoryID { get; set; }

        public Category? Category { get; set; }


        [NotMapped]

        public IFormFile? CoverImageFile { get; set; }


    }
}
