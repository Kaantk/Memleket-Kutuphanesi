using Azure.Identity;
using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace MemleketKütüphanesi_.Controllers
{
    public class DashboardController : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext(); // Veritabanı referans noktam

        public IActionResult Dashboard() // Dashboard sayfasında sadece sidebar olacak şekilde yükler.
        {
            return View();
        }

        public IActionResult _GetUserList() // User tablosunu listeler.
        {
            return View(dB.Users); // User tablosu içerisindeki bilgileri View'e gönderir.
        }

        public bool CheckUser(User model) // Kullanıcın User tablosunda olup olmadıgını kontrol eder.Kayıt varsa true,yoksa false döndürür.
        {
            string idNo = model.TcNo;

            var user = dB.Users.FirstOrDefault(u => u.TcNo == (idNo));

            if (user != null) // Kullanıcı varsa true dönecektir.
            {
                return true;
            }
            else // Kullanıcı yoksa false dönecektir.
            {
                return false;
            }

        }

        public User GetUserDetails(User model) // Tabloda yer alan kullanıcının bilgilerini geri döndürür.
        {
            var user = dB.Users.FirstOrDefault(x => x.TcNo.Equals(model.TcNo));

            ViewBag.name = user.Name;
            ViewBag.surname = user.Surname;
            ViewBag.tc = user.TcNo;
            ViewBag.email = user.Email;
            ViewBag.gender = user.Gender;
            ViewBag.birthday = user.BirthDay;
            ViewBag.phone = user.PhoneNumber;
            ViewBag.reports = user.ReportsTo;

            return user;
        }

        public User GetUser(User model) // Kullanıcı varsa bilgilerini yoksa hata mesajını döndürür.
        {
            bool CheckUserResult = CheckUser(model); // CheckUser kullanıcının User tablosunda olup olmamasına göre true veya false döndürecek.

            if (CheckUserResult == true) // Kullanıcı varsa modelden gelen kullanıcı bilgilerini geri döndürücem.
            {
                GetUserDetails(model);
                return model;
            }
            else // Eğer kullanıcı yoksa da hata mesajını geri döndürücem.
            {
                ViewBag.ErrorMessage = "Kullanıcı bulunamadı.Lütfen kontrol ediniz.";
                return model;
            }

        }

        #region _UserUpdate

        public IActionResult SetUserUpdate(User model) // Kullanıcının yeni girilen değerlerini kaydererek, günceller.
        {
            //var user = dB.Users.FirstOrDefault(x => x.TcNo.Equals(GetUser(model).TcNo));

            var user = dB.Users.FirstOrDefault(x => x.TcNo == model.TcNo);


            user.Name = model.Name;
            user.Surname = model.Surname;
            user.Email = model.Email;
            user.BirthDay = model.BirthDay;
            user.PhoneNumber = model.PhoneNumber;
            user.ReportsTo = model.ReportsTo;

            dB.SaveChanges();
            ViewBag.SuccessMessage = "Güncelleme işlemi başarılı.";
            return View("_UpdateUser");
        }

        [HttpGet]
        public IActionResult _UpdateUser() // Kullanıcı güncelleştirme sayfasını getiren action'dır.
        {
            return View("_UpdateUser");
        }

        [HttpPost]
        public IActionResult _UpdateUser(User model) // Kullanıcı güncelleştirme işlemi yapan action'dır.
        {
            GetUser(model);
            return View("_UpdateUser");
        }

        #endregion

        #region _UserDelete

        [HttpGet] 
        public IActionResult _DeleteUser() // Kullanıcı silme sayfasını getiren action'dır.
        {
            return View("_DeleteUser");
        }

        [HttpPost]
        public IActionResult _DeleteUser(User model) // Tc no kontrolü yapar kullanıcı varsa bilgilerini,yoksa hata mesajını döndürür.
        {
            GetUser(model);
            return View("_DeleteUser");
        }

        public IActionResult SetDeleteUser(User model) // Kullanıcıyı User tablosundan siler.
        {
            var user = dB.Users.FirstOrDefault(x => x.TcNo.Equals(GetUser(model).TcNo));
            dB.Users.Remove(user);
            dB.SaveChanges();
            ViewBag.DeleteSuccess = "Kullanıcı silme işlemi başarılı.";
            return View("_DeleteUser");
        }

        #endregion
    }
}
