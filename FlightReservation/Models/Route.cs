using System.ComponentModel.DataAnnotations;

namespace FlightReservation.Models
{
    public class Route
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Route name is required")]
        [StringLength(200, ErrorMessage = "Route name must be less than 200 characters")]
        public string Name { get; set; }

        // Flights listesi burada kalabilir. Bu, Route nesnesiyle ilişkili Flight nesnelerini içerir.
        // Validasyon bu liste üzerinde uygulanmaz çünkü liste yönetimi farklı bir mantık gerektirir.
        public List<FlightModel> Flights { get; set; }
    }
}
