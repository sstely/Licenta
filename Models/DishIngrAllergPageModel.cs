using Licenta.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Licenta.Models
{
    public class DishIngrAllergPageModel:PageModel
    {
        public List<AssignedIngredientData> AssignedIngredientDataList;
        public List<AssignedAllergenData> AssignedAllergenDataList;

        public void PopulateAssignedIngredientData(LicentaContext context, Dish dish)
        {
            var allIngredients = context.Ingredient;
            var dishIngredients = new HashSet<int>(dish.DishIngredients.Select(i => i.IngredientID));
            AssignedIngredientDataList = new List<AssignedIngredientData>();

            foreach (var ingr in allIngredients)
            {
                AssignedIngredientDataList.Add(new AssignedIngredientData
                {
                    IngredientID = ingr.ID,
                    Name = ingr.IngredientName,
                    Assigned = dishIngredients.Contains(ingr.ID)
                });
            }
        }

        public void PopulateAssignedAllergenData(LicentaContext context, Dish dish)
        {
            var allAllergens = context.Allergen;
            var dishAllergens = new HashSet<int>(dish.DishAllergens.Select(a => a.AllergenID));
            AssignedAllergenDataList = new List<AssignedAllergenData>();

            foreach (var alg in allAllergens)
            {
                AssignedAllergenDataList.Add(new AssignedAllergenData
                {
                    AllergenID = alg.ID,
                    Name = alg.AllergenName,
                    Assigned = dishAllergens.Contains(alg.ID)
                });
            }
        }

        public void UpdateDishIngredients(LicentaContext context, string[] selectedIngredients, Dish dishToUpdate)
        {
            if(selectedIngredients == null)
            {
                dishToUpdate.DishIngredients = new List<DishIngredient>();
                return;
            }

            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);
            var dishIngredients = new HashSet<int>(dishToUpdate.DishIngredients.Select(i => i.Ingredient.ID));

            foreach (var ingr in context.Ingredient)
            {
                if (selectedIngredientsHS.Contains(ingr.ID.ToString()))
                {
                    if (!dishIngredients.Contains(ingr.ID))
                    {
                        dishToUpdate.DishIngredients.Add(
                            new DishIngredient
                            {
                                DishID = dishToUpdate.ID,
                                IngredientID = ingr.ID
                            });
                    }
                }
                else
                {
                    if (dishIngredients.Contains(ingr.ID))
                    {
                        DishIngredient courseToRemove = dishToUpdate
                            .DishIngredients
                            .SingleOrDefault(i => i.IngredientID == ingr.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }

        public void UpdateDishAllergens(LicentaContext context, string[] selectedAllergens, Dish dishToUpdate)
        {
            if (selectedAllergens == null)
            {
                dishToUpdate.DishAllergens = new List<DishAllergen>();
                return;
            }

            var selectedAllergensHS = new HashSet<string>(selectedAllergens);
            var dishAllergens = new HashSet<int>(dishToUpdate.DishAllergens.Select(a => a.Allergen.ID));

            foreach (var alg in context.Allergen)
            {
                if (selectedAllergensHS.Contains(alg.ID.ToString()))
                {
                    if (!dishAllergens.Contains(alg.ID))
                    {
                        dishToUpdate.DishAllergens.Add(
                            new DishAllergen
                            {
                                DishID = dishToUpdate.ID,
                                AllergenID = alg.ID
                            });
                    }
                }
                else
                {
                    if (dishAllergens.Contains(alg.ID))
                    {
                        DishAllergen courseToRemove = dishToUpdate
                            .DishAllergens
                            .SingleOrDefault(i => i.AllergenID == alg.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
