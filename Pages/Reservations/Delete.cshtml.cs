﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly Licenta.Data.LicentaContext _context;

        public DeleteModel(Licenta.Data.LicentaContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Reservation Reservation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FirstOrDefaultAsync(m => m.ID == id);

            if (reservation == null)
            {
                return NotFound();
            }
            else 
            {
                Reservation = reservation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservation.FindAsync(id);

            if (reservation != null)
            {
                Reservation = reservation;
                _context.Reservation.Remove(Reservation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
