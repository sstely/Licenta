using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Dishes
{
    public class EditModel : DishIngrAllergPageModel
    {
        private readonly Licenta.Data.LicentaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(Licenta.Data.LicentaContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Dish Dish { get; set; } = default!;
        public string DishImageURL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Dish == null)
            {
                return NotFound();
            }

            var dish =  await _context.Dish
                .Include(d => d.Category)
                .Include(d => d.DishIngredients).ThenInclude(d => d.Ingredient)
                .Include(d => d.DishAllergens).ThenInclude(d => d.Allergen)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (dish == null)
            {
                return NotFound();
            }
            Dish = dish;

            DishImageURL = Dish.CoverImageURL;
            PopulateAssignedIngredientData(_context, Dish);
            PopulateAssignedAllergenData(_context, Dish);

            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "CategoryName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedIngredients, string[] selectedAllergens)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishToUpdate = await _context.Dish
                .Include(i => i.Category)
                .Include(i => i.DishIngredients).ThenInclude(i => i.Ingredient)
                .Include(i => i.DishAllergens).ThenInclude(i => i.Allergen)
                .FirstOrDefaultAsync(s => s.ID == id);

            if(dishToUpdate == null)
            {
                return NotFound();
            }

            if (Dish.CoverImageFile != null)
            {
                string folder = "covers/dishes/";
                folder += Guid.NewGuid().ToString() + "_" + Dish.CoverImageFile.FileName;

                dishToUpdate.CoverImageURL = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await Dish.CoverImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            }

            if (await TryUpdateModelAsync<Dish>(
                dishToUpdate, "Dish", 
                i => i.Name, 
                i => i.Description,
                i => i.Amount,
                i => i.Unit,
                i => i.ServingRecommendation,
                i => i.CoverImageURL,
                i => i.CategoryID))
            {
                UpdateDishIngredients(_context, selectedIngredients, dishToUpdate);
                UpdateDishAllergens(_context, selectedAllergens, dishToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateDishIngredients(_context, selectedIngredients, dishToUpdate);
            UpdateDishAllergens(_context, selectedAllergens, dishToUpdate);

            PopulateAssignedIngredientData(_context, dishToUpdate);
            PopulateAssignedAllergenData(_context, dishToUpdate);

            return Page();
        }
    }
}
