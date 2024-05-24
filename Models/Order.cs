using System.ComponentModel.DataAnnotations;

namespace Licenta.Models
{
    public class Order
    {
        public int ID { get; set; }

        public int? MemberID { get; set; }
        public Member? Member { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public ICollection<OrderDish>? OrderDishes { get; set; }

    }
}
