using System.ComponentModel.DataAnnotations;

namespace FlightReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Flight ID is required")]
        public int FlightId { get; set; }

        [Required(ErrorMessage = "Passenger name is required")]
        [StringLength(100, ErrorMessage = "Passenger name must be less than 100 characters")]
        public string PassengerName { get; set; }

        [Required(ErrorMessage = "Selected seat is required")]
        public string SelectedSeat { get; set; }
    }
}
