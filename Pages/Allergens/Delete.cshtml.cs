using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Allergens
{
    public class DeleteModel : PageModel
    {
        private readonly Licenta.Data.LicentaContext _context;

        public DeleteModel(Licenta.Data.LicentaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Allergen Allergen { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Allergen == null)
            {
                return NotFound();
            }

            var allergen = await _context.Allergen.FirstOrDefaultAsync(m => m.ID == id);

            if (allergen == null)
            {
                return NotFound();
            }
            else 
            {
                Allergen = allergen;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Allergen == null)
            {
                return NotFound();
            }
            var allergen = await _context.Allergen.FindAsync(id);

            if (allergen != null)
            {
                Allergen = allergen;
                _context.Allergen.Remove(Allergen);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
