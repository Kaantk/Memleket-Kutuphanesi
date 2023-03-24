using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemleketKütüphanesi_.Controllers
{
    public class BookWorksController : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext();

        public IActionResult _PartialGetBookList() // Kitap listesini getiren action'dır.
        {
            return View(dB.Books);
        }

        [HttpGet]
        public IActionResult _PartialAddBook()
        {
            return View("_PartialAddBook");
        }

        [HttpPost]
        public IActionResult _PartialAddBook(Book model) // Book tablosuna kitap ekleyip,kullanıcıya geri mesaj döndürür.
        {
            if (CheckBook(model) == true)
            {
                ViewBag.ErrorMessage = "Bu isimde bir kitap zaten bulunmaktadır.";
                return View("_PartialAddBook");
            }
            else
            {
                dB.Books.Add(model);
                dB.SaveChanges();
                ViewBag.SuccessMessage = "Kayıt işlemi başarılı.";
                return View("_PartialAddBook");
            }
        }

        public bool CheckBook(Book model) // Kullanıcının girdiği isimde bir kitabın daha önce kaydedilip,kaydedilmediğini kontrol eder.
        {
            var CheckResult = dB.Books.FirstOrDefault(x => x.Name.Equals(model.Name));

            if (CheckResult == null) // Bu durumda bu isimde bir kitap yok demektir
            {
                return false;
            }
            else // O zaman bu isimde bir kitap var demektir. 
                return true;
        }

        [HttpGet]
        public IActionResult _PartialDeleteBook()
        {
            return View("_PartialDeleteBook");
        }

        [HttpPost]
        public IActionResult _PartialDeleteBook(Book model)
        {
            if (CheckBook(model) == true) // Burada kitap kontrolü var demektir ve gelen kitap silinecektir.
            {
                var deleteBook = dB.Books.FirstOrDefault(x => x.Name.Equals(model.Name)); // İlk önce gelen kitabın nesnesi yakalanır.
                dB.Books.Remove(deleteBook);
                dB.SaveChanges();
                ViewBag.SuccessMessage = "Kitap silme işlemi başarılı.";
                return View("_PartialDeleteBook");
            }
            else
            {
                ViewBag.ErrorMessage = "Bu isimde bir kitap bulunmamaktadır.";
                return View("_PartialDeleteBook");
            }
        }
    }
}
