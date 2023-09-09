using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MemleketKütüphanesi_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Anasayfa() // Kütüphane anasayfasını yükler.
        {
            return View();
        }

        public IActionResult AboutPage() // Anasayfadaki Hakkımda kısmını yükler.
        {
            return View();
        }

        public IActionResult ContentPage() // Anasayfa iletişim kısmını yükler.
        {
            return View();
        }

    }
}