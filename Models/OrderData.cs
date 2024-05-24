namespace Licenta.Models
{
    public class OrderData
    {
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Dish> Dishes { get; set; }
        public IEnumerable<OrderDish> OrderDishes { get; set; }
    }
}
