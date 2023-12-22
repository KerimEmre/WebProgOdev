using FlightReservation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;


public class FlightController : Controller
{
    private readonly List<FlightReservation.Models.FlightModel> _flights = new List<FlightReservation.Models.FlightModel>
    {
        new FlightReservation.Models.FlightModel { Id = 1, RouteId = 1, AircraftId = 1, DepartureTime = DateTime.Now.AddHours(2) },
        new FlightReservation.Models.FlightModel { Id = 2, RouteId = 2, AircraftId = 2, DepartureTime = DateTime.Now.AddHours(4) },
        // Diğer uçuşları buraya ekle
    };

    private readonly List<FlightReservation.Models.Route> _routes = new List<FlightReservation.Models.Route>
    {
        new FlightReservation.Models.Route { Id = 1, Name = "İstanbul - Paris" },
        new FlightReservation.Models.Route { Id = 2, Name = "New York - Los Angeles" },
        // Diğer güzergahları buraya ekle
    };

    private readonly List<Aircraft> _aircrafts = new List<Aircraft>
    {
        new Aircraft { Id = 1, Name = "Boeing 737", Capacity = 150, SeatLayout = "3+3" },
        new Aircraft { Id = 2, Name = "Airbus A320", Capacity = 180, SeatLayout = "3+3" },
        // Diğer uçakları buraya ekle
    };

    // Tüm uçuşları listeleyen bir sayfa
    public IActionResult Index()
    {
        var flights = _flights.Select(flight => new
        {
            Id = flight.Id,
            RouteName = _routes.FirstOrDefault(r => r.Id == flight.RouteId)?.Name,
            AircraftName = _aircrafts.FirstOrDefault(a => a.Id == flight.AircraftId)?.Name,
            DepartureTime = flight.DepartureTime
        }).ToList();

        return View(flights);
    }

    // Uçuş detaylarını gösteren bir sayfa
    public IActionResult Details(int id)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == id);
        if (flight == null)
        {
            return NotFound(); // Eğer uçuş bulunamazsa 404 
        }

        var route = _routes.FirstOrDefault(r => r.Id == flight.RouteId);
        var aircraft = _aircrafts.FirstOrDefault(a => a.Id == flight.AircraftId);

        var model = new
        {
            Id = flight.Id,
            RouteName = route?.Name,
            AircraftName = aircraft?.Name,
            DepartureTime = flight.DepartureTime
        };

        return View(model);
    }

    // Yeni uçuş ekleme sayfası
    public IActionResult Create()
    {
        var routeSelectList = _routes.Select(route => new SelectListItem
        {
            Value = route.Id.ToString(),
            Text = route.Name
        });

        var aircraftSelectList = _aircrafts.Select(aircraft => new SelectListItem
        {
            Value = aircraft.Id.ToString(),
            Text = aircraft.Name
        });

        ViewData["Routes"] = routeSelectList;
        ViewData["Aircrafts"] = aircraftSelectList;

        return View();
    }

    // Yeni uçuş eklemek için HTTP POST isteği
    [HttpPost]
    public IActionResult Create(FlightModel flight)
    {
        if (ModelState.IsValid)
        {
            _flights.Add(flight);
            return RedirectToAction("Index");
        }

        // Eğer model validasyonu başarısızsa, kullanıcıya formu tekrar göster
        var routeSelectList = _routes.Select(route => new SelectListItem
        {
            Value = route.Id.ToString(),
            Text = route.Name
        });

        var aircraftSelectList = _aircrafts.Select(aircraft => new SelectListItem
        {
            Value = aircraft.Id.ToString(),
            Text = aircraft.Name
        });

        ViewData["Routes"] = routeSelectList;
        ViewData["Aircrafts"] = aircraftSelectList;

        return View(flight);
    }

    // Güncelleme sayfası
    public IActionResult Edit(int id)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == id);
        if (flight == null)
        {
            return NotFound();
        }

        var routeSelectList = _routes.Select(route => new SelectListItem
        {
            Value = route.Id.ToString(),
            Text = route.Name,
            Selected = route.Id == flight.RouteId
        });

        var aircraftSelectList = _aircrafts.Select(aircraft => new SelectListItem
        {
            Value = aircraft.Id.ToString(),
            Text = aircraft.Name,
            Selected = aircraft.Id == flight.AircraftId
        });

        ViewData["Routes"] = routeSelectList;
        ViewData["Aircrafts"] = aircraftSelectList;

        return View(flight);
    }

    // Güncelleme işlemi için HTTP POST isteği
    [HttpPost]
    public IActionResult Edit(int id, FlightModel updatedFlight)
    {
        var flight = _flights.FirstOrDefault(f => f.Id == id);
        if (flight == null)
        {
            return NotFound();
        }

        // Güncelleme işlemini gerçekleştir
        flight.RouteId = updatedFlight.RouteId;
        flight.AircraftId = updatedFlight.AircraftId;
        flight.DepartureTime = updatedFlight.DepartureTime;

        return RedirectToAction("Index");
    }
}
