using System.ComponentModel.DataAnnotations;

namespace Licenta.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        public int? MemberID { get; set; }

        public Member? Member { get; set; }

        public int NumberPeople { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan ReservationTime { get; set; }


    }
}
