namespace Licenta.Models
{
    public class DishIngredient
    {

        public int ID { get; set; }

        public int DishID { get; set; }

        public Dish Dish { get; set; }

        public int IngredientID { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
