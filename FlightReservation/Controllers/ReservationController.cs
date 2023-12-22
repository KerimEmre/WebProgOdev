using FlightReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


public class ReservationController : Controller
{
    private readonly List<FlightReservation.Models.Reservation> _reservations = new List<FlightReservation.Models.Reservation>();
    private readonly List<FlightReservation.Models.FlightModel> _flights = new List<FlightReservation.Models.FlightModel>
    {
        new FlightModel { Id = 1, RouteId = 1, AircraftId = 1, DepartureTime = DateTime.Now.AddHours(2), Aircraft = new Aircraft() },
        new FlightModel { Id = 2, RouteId = 2, AircraftId = 2, DepartureTime = DateTime.Now.AddHours(4), Aircraft = new Aircraft() },
        // Diğer uçuşları buraya ekleyeceksin
    };

    // Koltuk seçme sayfası
    public IActionResult SelectSeat(int flightId)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == flightId);
        if (flight == null)
        {
            return NotFound();
        }

        // Rezervasyon yapmak için kullanılacak koltukları listeleme (örneğin, 3+3 koltuk düzeni)
        var seatLayout = flight.Aircraft.SeatLayout.Split('+');
        var rowCount = int.Parse(seatLayout[0]);
        var columnCount = int.Parse(seatLayout[1]);

        var seats = new List<string>();
        for (int row = 1; row <= rowCount; row++)
        {
            for (int column = 1; column <= columnCount; column++)
            {
                seats.Add($"{row}-{column}");
            }
        }

        ViewData["Seats"] = seats;

        return View();
    }

    // Koltuk seçme işlemi için HTTP POST isteği
    [HttpPost]
    public IActionResult SelectSeat(int flightId, string selectedSeat, string passengerName)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == flightId);
        if (flight == null)
        {
            return NotFound();
        }

        // Burada daha fazla validasyon ekleyebilirsiniz (örneğin, koltuk zaten alınmış mı diye kontrol etmek gibi)
        if (ModelState.IsValid)
        {
            var reservation = new Reservation
            {
                FlightId = flightId,
                PassengerName = passengerName,
                SelectedSeat = selectedSeat
            };

            _reservations.Add(reservation);

            return RedirectToAction("Index");
        }

        // Validasyon hatası durumunda gerekli verileri tekrar yükle
        // Rezervasyon yapmak için kullanılacak koltukları listeleme (örneğin, 3+3 koltuk düzeni)
        var seatLayout = flight.Aircraft.SeatLayout.Split('+');
        var rowCount = int.Parse(seatLayout[0]);
        var columnCount = int.Parse(seatLayout[1]);

        var seats = new List<string>();
        for (int row = 1; row <= rowCount; row++)
        {
            for (int column = 1; column <= columnCount; column++)
            {
                seats.Add($"{row}-{column}");
            }
        }

        ViewData["Seats"] = seats;

        return View();
    }

    // Tüm rezervasyonları listeleyen bir sayfa
    public IActionResult Index()
    {
        return View(_reservations);
    }
}
