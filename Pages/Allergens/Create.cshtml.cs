using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Allergens
{
    public class CreateModel : PageModel
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
            return Page();
        }

        [BindProperty]
        public Allergen Allergen { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Allergen == null || Allergen == null)
            {
                return Page();
            }

          if (Allergen.AllergenImageFile != null)
            {
                string folder = "covers/allergens/";
                folder += Guid.NewGuid().ToString() + "_" + Allergen.AllergenImageFile.FileName;

                Allergen.AllergenImageURL = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await Allergen.AllergenImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            }

            _context.Allergen.Add(Allergen);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
