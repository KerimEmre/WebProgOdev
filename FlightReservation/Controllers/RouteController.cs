using Microsoft.AspNetCore.Mvc;
using FlightReservation.Models;

public class RouteController : Controller
{
    private readonly List<FlightReservation.Models.Route> _routes = new List<FlightReservation.Models.Route>
    {
        new FlightReservation.Models.Route { Id = 1, Name = "İstanbul - Paris" },
        new FlightReservation.Models.Route { Id = 2, Name = "New York - Los Angeles" },
        
    };

    // Tüm güzergahları listeleyen bir sayfa
    public IActionResult Index()
    {
        return View(_routes);
    }

    // Güzergah detaylarını gösteren sayfa
    public IActionResult Details(int id)
    {
        var route = _routes.FirstOrDefault(r => r.Id == id);
        if (route == null)
        {
            return NotFound(); // Eğer güzergah bulunamazsa 404 hata veriyoruz
        }

        return View(route);
    }

    // Yeni güzergah ekleme sayfası
    public IActionResult Create()
    {
        return View();
    }

    // Yeni güzergah eklemek için HTTP POST isteği
    [HttpPost]
    public IActionResult Create(FlightReservation.Models.Route route)
    {
        if (ModelState.IsValid)
        {
            _routes.Add(route);
            return RedirectToAction("Index");
        }

        // Validasyon hatası durumunda formu tekrar göster
        return View(route);
    }

    // Güncelleme sayfası
    public IActionResult Edit(int id)
    {
        var route = _routes.FirstOrDefault(r => r.Id == id);
        if (route == null)
        {
            return NotFound();
        }

        return View(route);
    }

    // Güncelleme işlemi için HTTP POST isteği
    [HttpPost]
    public IActionResult Edit(int id, FlightReservation.Models.Route updatedRoute)
    {
        var route = _routes.FirstOrDefault(r => r.Id == id);
        if (route == null)
        {
            return NotFound();
        }

        // Güncelleme işlemini gerçekleştirme
        route.Name = updatedRoute.Name;

        return RedirectToAction("Index");
    }
}
