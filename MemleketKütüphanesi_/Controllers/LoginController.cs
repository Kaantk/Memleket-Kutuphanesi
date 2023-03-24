using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemleketKütüphanesi_.Controllers
{
    public class LoginController : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext(); // Database'e ulaşmak için referans noktası

        // Kullanıcı giriş kontrolleri burada yer alır.
        #region _PartialUserLogin

        [HttpGet]
        public IActionResult _PartialUserLogin() // Kullanıcı giriş sayfası bu action'la gelir.
        {
            return View("_PartialUserLogin");
        }

        [HttpPost] // Bu action için Validation kuralları eklenmiştir.
        public IActionResult _PartialUserLogin(User model) // Kullanıcının girdiği e-posta ve şifre bilgileri bu model ile action'a taşınır.
        {
            var UserCheckResult = dB.Users.FirstOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
            // İki bilgide aynı anda karşılanıyorsa bu kullanıcının verileri değişkene aktarılır.

            if (UserCheckResult != null) // Null dönmediği durumda kullanıcı var demektir ve ilgili dashboard sayfasına yönlendirilir.
            {
                return RedirectToAction("UserDashboardLayout", "UserDashboard");
            }
            else // Buraya gelindiğinde bu bilgilerle eşleşen bir kullanıcı yoktur ve hata mesajı döndürülür.
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı";
                return View("_PartialUserLogin");
            }
        }

        #endregion

        // Kullanıcı kayıt işlemleri burada yer alır.
        #region _PartialUserRegister 

        [HttpGet] 
        public IActionResult _PartialUserRegister() // Kullanıcı kayıt sayfasını bu action yükler.
        {
            return View();
        }

        [HttpPost]
        public IActionResult _PartialUserRegister(User model) // Kullanıcıdan gelen bilgiler bu model ile action'a taşınır.
        {
            if (CheckTcNo(model) == true) // True döndüğünde bu Tc ile kayıtlı kullanıcı var demektir.
            {
                ViewBag.TcErrorMessage = "Bu Tc ile kayitli kullanici bulunmaktadir.";
                ViewBag.ErrorRegisterMessage = "Lütfen alanları doğru bir şekilde doldurunuz.";
                return View("_PartialUserRegister");
            }
            else if (CheckEmail(model) == true) // True döndüğünde bu E-mail ile kayıtlı kullanıcı var demektir.
            {
                ViewBag.EmailErrorMessage = "Bu E-posta ile kayitli kullanici bulunmaktadir.";
                ViewBag.ErrorRegisterMessage = "Lütfen alanları doğru bir şekilde doldurunuz.";
                return View("_PartialUserRegister");
            }
            else // Bu durumda bu bilgilerle kayıtlı kullanıcı yoktur ve yeni kullanıcı kaydı yapılır.
            {
                dB.Users.Add(model);
                dB.SaveChanges();
                ViewBag.SuccessRegisterMessage = "Kullanıcı kayıt işlemi başarılı.";
                return View("_PartialUserRegister");
            }
        }

        public bool CheckTcNo(User model) // Kayıt olucak kullanıcının Tc numarasının daha önceden olup olmadığını kontrol eder.
        {
            var ResultCheckTc = dB.Users.FirstOrDefault(x => x.TcNo.Equals(model.TcNo));
            // Eğer ki kullanıcı varsa kullanıcı bilgilerini yoksa null döndürecek.

            if (ResultCheckTc != null) // Benim zaten bu durumda bu Tc ile kayıtlı kullanıcım var demektir.
                return true;
            else // Bu durumda bu Tc ile kayıtlı kullanıcı yok demektir.
                return false;
        }

        public bool CheckEmail(User model) // Kayıt olucak kullanıcının E-postasının daha önceden olup olmadığını kontrol eder.
        {
            var ResultCheckEmail = dB.Users.FirstOrDefault(x => x.Email.Equals(model.Email));
            // Eğer ki kullanıcı varsa kullanıcı bilgilerini yoksa null döndürecek.

            if (ResultCheckEmail != null) // Benim zaten bu durumda bu e-posta ile kayıtlı kullanıcım var demektir.
                return true;
            else // Bu durumda bu e-posta ile kayıtlı kullanıcı yoktur.
                return false;
        }

        #endregion

        // Personel giriş kontrolleri burada yer alır.
        #region _PartialEmployeeLogin

        [HttpGet] // _PartialEmployeeLogin'e gelen istekleri bu action karşılıcak.
        public IActionResult _PartialEmployeeLogin()
        {
            return View();
        }

        [HttpPost] // _PartialEmployeeLogin'e formdan gelen verileri bu action karşılıcak.
        public IActionResult _PartialEmployeeLogin(Employee model)
        {
            var employeecontrol = dB.Employees.FirstOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
            // Kullanıcı varsa kullanıcı bilgilerini yoksa null döndürür.

            if (employeecontrol != null) // Bu durumda personel bilgileri doğru girilmiş demektir.
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else // Bu durumda da personel bilgileri yanlış girilmiş demektir.
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return View("_PartialEmployeeLogin");
            }
        }

        #endregion

    }
}
