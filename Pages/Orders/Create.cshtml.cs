using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Orders
{
    public class CreateModel : OrderDishesPageModel
    {
        private readonly Licenta.Data.LicentaContext _context;

        public CreateModel(Licenta.Data.LicentaContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["MemberID"] = new SelectList(_context.Set<Member>(), "ID", "FullName");

            var order = new Order();
            order.OrderDishes = new List<OrderDish>();
            PopulateAssignedDishData(_context, order);

            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedDishes)
        {
            var newOrder = new Order();

            if (selectedDishes != null)
            {
                newOrder.OrderDishes = new List<OrderDish>();
                foreach (var dish in selectedDishes)
                {
                    var dishToAdd = new OrderDish
                    {
                        DishID = int.Parse(dish)
                    };
                    newOrder.OrderDishes.Add(dishToAdd);
                }
            }

            Order.OrderDishes = newOrder.OrderDishes;

            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
