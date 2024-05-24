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

namespace Licenta.Pages.Orders
{
    public class EditModel : OrderDishesPageModel
    {
        private readonly Licenta.Data.LicentaContext _context;

        public EditModel(Licenta.Data.LicentaContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order =  await _context.Order
                .Include(o => o.Member)
                .Include(o => o.OrderDishes).ThenInclude(o => o.Dish)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (order == null)
            {
                return NotFound();
            }
            Order = order;

            PopulateAssignedDishData(_context, Order);

            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "ID", "FullName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedDishes)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderToUpdate = await _context.Order
                .Include(i => i.Member)
                .Include(i => i.OrderDishes).ThenInclude(i => i.Dish)
                .FirstOrDefaultAsync(s => s.ID == id);

            if (orderToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Order>(
                orderToUpdate, "Order",
                i => i.OrderDate,
                i => i.MemberID))
            {
                UpdateOrderDishes(_context, selectedDishes, orderToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateOrderDishes(_context, selectedDishes, orderToUpdate);
            PopulateAssignedDishData(_context, orderToUpdate);

            return Page();
        }
    }
}
