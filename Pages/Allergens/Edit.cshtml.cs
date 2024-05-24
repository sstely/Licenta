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

namespace Licenta.Pages.Allergens
{
    public class EditModel : PageModel
    {
        private readonly Licenta.Data.LicentaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(Licenta.Data.LicentaContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Allergen Allergen { get; set; } = default!;
        public string AllergenImageURL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Allergen == null)
            {
                return NotFound();
            }

            var allergen =  await _context.Allergen.FirstOrDefaultAsync(m => m.ID == id);
            if (allergen == null)
            {
                return NotFound();
            }
            Allergen = allergen;

            AllergenImageURL = Allergen.AllergenImageURL;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Allergen).State = EntityState.Modified;

            if (Allergen.AllergenImageFile != null)
            {
                string folder = "covers/allergens/";
                folder += Guid.NewGuid().ToString() + "_" + Allergen.AllergenImageFile.FileName;

                Allergen.AllergenImageURL = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await Allergen.AllergenImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllergenExists(Allergen.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AllergenExists(int id)
        {
          return (_context.Allergen?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
