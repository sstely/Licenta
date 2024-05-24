namespace Licenta.Models
{
    public class DishData
    {
        public IEnumerable<Dish> Dishes { get; set; }

        public IEnumerable<Ingredient> Ingredients { get; set;}
        public IEnumerable<DishIngredient> DishIngredients { get; set; }

        public IEnumerable<Allergen> Allergens { get; set;}
        public IEnumerable<DishAllergen> DishAllergens { get; set; }


    }
}
