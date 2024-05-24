﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Categories
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
        public Category Category { get; set; } = default!;
        public string CategoryImageURL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category =  await _context.Category.FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;

            CategoryImageURL = Category.CategoryImageURL;

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

            _context.Attach(Category).State = EntityState.Modified;

            if (Category.CategoryImageFile != null)
            {
                string folder = "covers/categories/";
                folder += Guid.NewGuid().ToString() + "_" + Category.CategoryImageFile.FileName;

                Category.CategoryImageURL = "/" + folder;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await Category.CategoryImageFile.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.ID))
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

        private bool CategoryExists(int id)
        {
          return (_context.Category?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
