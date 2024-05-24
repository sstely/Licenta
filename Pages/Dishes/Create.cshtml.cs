using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Licenta.Data;
using Licenta.Models;
using Microsoft.AspNetCore.Hosting;

namespace Licenta.Pages.Dishes
{
    public class CreateModel : DishIngrAllergPageModel
    {
        private readonly Licenta.Data.LicentaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(Licenta.Data.LicentaContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "CategoryName");

            var dish = new Dish();
            dish.DishIngredients = new List<DishIngredient>();
            dish.DishAllergens = new List<DishAllergen>();

            PopulateAssignedIngredientData(_context, dish);
            PopulateAssignedAllergenData(_context, dish);

            return Page();
        }

        [BindProperty]
        public Dish Dish { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedIngredients, string[] selectedAllergens)
        {
            var newDish = new Dish();

            if(selectedIngredients != null)
            {
                newDish.DishIngredients = new List<DishIngredient>();
                foreach(var ingr in selectedIngredients)
                {
                    var ingrToAdd = new DishIngredient
                    {
                        IngredientID = int.Parse(ingr)
                    };
                    newDish.DishIngredients.Add(ingrToAdd);
                }
            }
            if (selectedAllergens != null)
            {
                newDish.DishAllergens = new List<DishAllergen>();
                foreach (var alg in selectedAllergens)
                {
                    var algToAdd = new DishAllergen
                    {
                        AllergenID = int.Parse(alg)
                    };
                    newDish.DishAllergens.Add(algToAdd);
                }
            }

            Dish.DishIngredients = newDish.DishIngredients;
            Dish.DishAllergens = newDish.DishAllergens;

            if (Dish.CoverImageFile != null)
            {
                string folder = "covers/dishes/";
                folder += Guid.NewGuid().ToString() + "_" + Dish.CoverImageFile.FileName;

                Dish.CoverImageURL = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await Dish.CoverImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            }

            _context.Dish.Add(Dish);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
