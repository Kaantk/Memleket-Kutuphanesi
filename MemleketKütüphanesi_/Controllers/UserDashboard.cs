using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemleketKütüphanesi_.Controllers
{
    public class UserDashboard : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext();

        public IActionResult _UserDashboardLayout() // Kullanıcı dashboardu burada yüklenir.
        {
            return View();
        }

        [HttpGet]
        public IActionResult _GetBookList() // Kitap listesini getiren action'dır.
        {
            return View(dB.Books);
        }
    }
}
