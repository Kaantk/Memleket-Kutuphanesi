using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemleketKütüphanesi_.Controllers
{
    public class UserDashboard : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext();

        public IActionResult UserDashboardLayout()
        {
            return View();
        }

        [HttpGet]
        public IActionResult _PartialGetBookList() // Kitap listesini getiren action'dır.
        {
            return View(dB.Books);
        }
    }
}
