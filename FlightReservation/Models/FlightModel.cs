using System.ComponentModel.DataAnnotations;

namespace FlightReservation.Models
{
    public class FlightModel
    {
        public int Id { get; set; }

        [Required]
        public Aircraft Aircraft { get; set; }

        [Required(ErrorMessage = "Route ID is required")]
        public int RouteId { get; set; }

        public string RouteName { get; set; }

        [Required(ErrorMessage = "Aircraft ID is required")]
        public int AircraftId { get; set; }

        [Required(ErrorMessage = "Departure time is required")]
        public DateTime DepartureTime { get; set; }
    }
}
