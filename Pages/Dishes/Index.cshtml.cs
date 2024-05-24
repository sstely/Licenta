using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Dishes
{
    public class IndexModel : PageModel
    {
        private readonly Licenta.Data.LicentaContext _context;

        public IndexModel(Licenta.Data.LicentaContext context)
        {
            _context = context;
        }

        public IList<Dish> Dish { get;set; } = default!;
        public DishData DishD {  get; set; }
        public int DishID { get; set; }
        public int IngredientID { get; set; }
        public int AllergenID { get; set; }

        public async Task OnGetAsync(int? id, int? ingredientID, int? allergenID)
        {
            DishD = new DishData();

            DishD.Dishes = await _context.Dish
                .Include(d => d.Category)
                .Include(d => d.DishIngredients).ThenInclude(d => d.Ingredient)
                .Include(d => d.DishAllergens).ThenInclude(d => d.Allergen)
                .AsNoTracking()
                .OrderBy(d => d.Name)
                .ToListAsync();

            if (id != null)
            {
                DishID = id.Value;
                Dish dish = DishD.Dishes
                    .Where(i => i.ID == id.Value).Single();
                DishD.Ingredients = dish.DishIngredients.Select(s => s.Ingredient);
                DishD.Allergens = dish.DishAllergens.Select(s => s.Allergen);

            }
        }
    }
}
