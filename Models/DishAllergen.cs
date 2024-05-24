namespace Licenta.Models
{
    public class DishAllergen
    {
        public int Id { get; set; } 

        public int DishID { get; set; }

        public Dish Dish { get; set; }

        public int AllergenID { get; set; }

        public Allergen Allergen { get; set; }
    }
}
