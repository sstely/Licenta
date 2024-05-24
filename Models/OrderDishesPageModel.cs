using Licenta.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Licenta.Models
{
    public class OrderDishesPageModel:PageModel
    {
        public List<AssignedDishData> AssignedDishDataList;

        public void PopulateAssignedDishData(LicentaContext context, Order order)
        {
            var allDishes = context.Dish;
            var orderDishes = new HashSet<int>(order.OrderDishes.Select(d => d.DishID));
            AssignedDishDataList = new List<AssignedDishData>();

            foreach (var dish in allDishes)
            {
                AssignedDishDataList.Add(new AssignedDishData
                {
                    DishID = dish.ID,
                    Name = dish.Name,
                    Assigned = orderDishes.Contains(dish.ID)
                });
            }
        }

        public void UpdateOrderDishes(LicentaContext context, string[] selectedDishes, Order orderToUpdate)
        {
            if (selectedDishes == null)
            {
                orderToUpdate.OrderDishes = new List<OrderDish>();
                return;
            }

            var selectedDishesHS = new HashSet<string>(selectedDishes);
            var orderDishes = new HashSet<int>(orderToUpdate.OrderDishes.Select(d => d.Dish.ID));

            foreach (var dish in context.Dish)
            {
                if (selectedDishesHS.Contains(dish.ID.ToString()))
                {
                    if (!orderDishes.Contains(dish.ID))
                    {
                        orderToUpdate.OrderDishes.Add(
                            new OrderDish
                            {
                                OrderID = orderToUpdate.ID,
                                DishID = dish.ID
                            });
                    }
                }
                else
                {
                    if (orderDishes.Contains(dish.ID))
                    {
                        OrderDish courseToRemove = orderToUpdate
                            .OrderDishes
                            .SingleOrDefault(i => i.DishID == dish.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
