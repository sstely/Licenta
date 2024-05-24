namespace Licenta.Models
{
    public class OrderDish
    {
        public int ID { get; set; }

        public int OrderID { get; set; }

        public Order Order { get; set; }

        public int DishID { get; set; }

        public Dish Dish { get; set; }
    }
}
