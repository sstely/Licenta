using System.ComponentModel.DataAnnotations;

namespace Licenta.Models
{
    public class Member
    {
        public int ID { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string Email { get; set; }

        public string? Phone { get; set; }

        [Display(Name = "Nume intreg")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }


        public ICollection<Order>? Orders { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
    }
}
