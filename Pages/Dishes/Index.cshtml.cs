﻿using System;
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

        public async Task OnGetAsync()
        {
            if (_context.Dish != null)
            {
                Dish = await _context.Dish
                    .Include(d => d.Category)
                    .ToListAsync();
            }
        }
    }
}
