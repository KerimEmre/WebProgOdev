using FlightReservation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


public class AircraftController : Controller
{
    private readonly List<FlightReservation.Models.Aircraft> _aircrafts = new List<FlightReservation.Models.Aircraft>
    {
        new FlightReservation.Models.Aircraft { Id = 1, Name = "Boeing 737", Capacity = 150, SeatLayout = "3+3" },
        new FlightReservation.Models.Aircraft { Id = 2, Name = "Airbus A320", Capacity = 180, SeatLayout = "3+3" },
        // Diğer uçakları buraya ekle
    };

    // Tüm uçakları listeleyen bir sayfa
    public IActionResult Index()
    {
        return View(_aircrafts);
    }

    // Uçak detaylarını gösteren bir sayfa
    public IActionResult Details(int id)
    {
        var aircraft = _aircrafts.FirstOrDefault(a => a.Id == id);
        if (aircraft == null)
        {
            return NotFound(); // Eğer uçak bulunamazsa 404 
        }

        return View(aircraft);
    }

    // Yeni uçak ekleme sayfası
    public IActionResult Create()
    {
        return View();
    }

    // Yeni uçak eklemek için HTTP POST isteği
    [HttpPost]
    public IActionResult Create(Aircraft aircraft)
    {
        if (ModelState.IsValid)
        {
            _aircrafts.Add(aircraft);
            return RedirectToAction("Index");
        }

        return View(aircraft); // Eğer validasyon hatası varsa formu tekrar göster
    }

    // Güncelleme sayfası
    public IActionResult Edit(int id)
    {
        var aircraft = _aircrafts.FirstOrDefault(a => a.Id == id);
        if (aircraft == null)
        {
            return NotFound();
        }

        return View(aircraft);
    }

    // Güncelleme işlemi için HTTP POST isteği
    [HttpPost]
    public IActionResult Edit(int id, Aircraft updatedAircraft)
    {
        var aircraft = _aircrafts.FirstOrDefault(a => a.Id == id);
        if (aircraft == null)
        {
            return NotFound();
        }

        // Güncelleme işlemini gerçekleştir
        aircraft.Name = updatedAircraft.Name;
        aircraft.Capacity = updatedAircraft.Capacity;
        aircraft.SeatLayout = updatedAircraft.SeatLayout;

        return RedirectToAction("Index");
    }
}
