using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Models
{
    public class Ingredient
    {
        public int ID { get; set; }

        [Display(Name = "Ingredient")]
        public string IngredientName { get; set; }

        [Display(Name = "Cantitate")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal Amount { get; set; }

        [Display(Name = "Unitate masura")]
        public string Unit { get; set; }

        [Display(Name = "Pret ingredient")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal IngredientPrice { get; set; }

        [Display(Name = "Forma prezentare")]
        public string? IngredientPresentation { get; set; }



        [Display(Name = "Calorii")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Calories { get; set; }

        [Display(Name = "Grasimi saturate")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? SaturatedFats { get; set; }

        [Display(Name = "Grasimi nesaturate")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? UnsaturatedFats { get; set; }

        [Display(Name = "Zaharuri")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Sugars { get; set; }

        [Display(Name = "Proteine")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Proteins { get; set; }

        [Display(Name = "Sare")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Salt { get; set; }

        [Display(Name = "Fibre")]
        [Column(TypeName = "decimal(6,2)")]
        public decimal? Fibers { get; set; }


        public ICollection<DishIngredient>? DishIngredients { get; set; }

    }
}
