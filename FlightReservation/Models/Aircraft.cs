using System.ComponentModel.DataAnnotations;

namespace FlightReservation.Models
{
    public class Aircraft
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Aircraft name is required")]
        public string Name { get; set; }

        [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Seat layout is required")]
        public string SeatLayout { get; set; }
    }
}
