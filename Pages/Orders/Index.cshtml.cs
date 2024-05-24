using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Licenta.Data;
using Licenta.Models;

namespace Licenta.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Licenta.Data.LicentaContext _context;

        public IndexModel(Licenta.Data.LicentaContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;
        public OrderData OrderD { get; set; }
        public int OrderID { get; set; }
        public int DishID { get; set; }

        public async Task OnGetAsync(int? id, int? dishID)
        {
            OrderD = new OrderData();

            OrderD.Orders = await _context.Order
                .Include(o => o.Member)
                .Include(o => o.OrderDishes).ThenInclude(o => o.Dish)
                .AsNoTracking()
                .ToListAsync();

            if (id != null)
            {
                OrderID = id.Value;
                Order order = OrderD.Orders
                    .Where(i => i.ID == id.Value).Single();
                OrderD.Dishes = order.OrderDishes.Select(s => s.Dish);

            }
        }
    }
}
