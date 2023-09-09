using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemleketKütüphanesi_.Controllers
{
    public class BookWorksController : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext();

        public IActionResult _GetBookList() // Kitap listesini getiren action'dır.
        {
            return View(dB.Books);
        }

        [HttpGet]
        public IActionResult _AddBook() // Kitap ekleme sayfasını getiren action'dır.
        {
            return View("_AddBook");
        }

        [HttpPost]
        public IActionResult _AddBook(Book model) // Book tablosuna kitap ekleyip,kullanıcıya geri mesaj döndürür.
        {
            if (CheckBook(model) == true) // Daha önceden aynı isimde bir kitap varsa buraya girer.
            {
                ViewBag.ErrorMessage = "Bu isimde bir kitap zaten bulunmaktadır.";
                return View("_AddBook");
            }
            else // Daha önce aynı isimde bir kitap kaydı yapılmadıysa kayıt işlemini yapar.
            {
                dB.Books.Add(model);
                dB.SaveChanges();
                ViewBag.SuccessMessage = "Kayıt işlemi başarılı.";
                return View("_AddBook");
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
        public IActionResult _DeleteBook() // Kitap silme sayfasını getiren action'dır.
        {
            return View("_DeleteBook");
        }

        [HttpPost]
        public IActionResult _DeleteBook(Book model) // Kitap silme işlemi burada yapılır.
        {
            if (CheckBook(model) == true) // Burada kitap kontrolü yapılır ve eğer kitap bulunursa silinecektir.
            {
                var deleteBook = dB.Books.FirstOrDefault(x => x.Name.Equals(model.Name)); // İlk önce gelen kitabın nesnesi yakalanır.
                dB.Books.Remove(deleteBook);
                dB.SaveChanges();
                ViewBag.SuccessMessage = "Kitap silme işlemi başarılı.";
                return View("_DeleteBook");
            }
            else
            {
                ViewBag.ErrorMessage = "Bu isimde bir kitap bulunmamaktadır.";
                return View("_DeleteBook");
            }
        }
    }
}
