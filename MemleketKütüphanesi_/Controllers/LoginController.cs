using MemleketKütüphanesi_.Models;
using Microsoft.AspNetCore.Mvc;

namespace MemleketKütüphanesi_.Controllers
{
    public class LoginController : Controller
    {
        MemleketLibraryContext dB = new MemleketLibraryContext(); // Database'e ulaşmak için referans noktası

        // Kullanıcı giriş kontrolleri burada yer alır.
        #region _UserLogin

        [HttpGet]
        public IActionResult _UserLogin() // Kullanıcı giriş sayfası bu action'la gelir.
        {
            return View("_UserLogin");
        }

        [HttpPost] // Bu action için Validation kuralları eklenmiştir.
        public IActionResult _UserLogin(User model) // Kullanıcının girdiği e-posta ve şifre bilgileri view'den gelen bu model ile action'a taşınır.
        {
            var UserCheckResult = dB.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
            // İki bilgide aynı anda karşılanıyorsa bu kullanıcının verileri değişkene aktarılır.

            if (UserCheckResult != null) // Null dönmediği durumda kullanıcı var demektir ve ilgili dashboard sayfasına yönlendirilir.
            {
                return RedirectToAction("_UserDashboardLayout", "UserDashboard");
            }
            else if (model.Email == null && model.Password == null) // Eğer ki e-posta ve şifre aynı anda boş girilirse sayfa tekrar yüklenir.
            {
                return View("_UserLogin");
            }
            else // Buraya gelindiğinde bu bilgilerle eşleşen bir kullanıcı yoktur ve hata mesajı döndürülür.
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı";
                return View("_UserLogin");
            }
        }

        #endregion

        // Kullanıcı kayıt işlemleri burada yer alır.
        #region _UserRegister 

        [HttpGet] 
        public IActionResult _UserRegister() // Kullanıcı kayıt sayfasını bu action yükler.
        {
            return View();
        }

        [HttpPost]
        public IActionResult _UserRegister(User model) // Kullanıcıdan gelen bilgiler bu model ile action'a taşınır.
        {
            if (CheckTcNo(model) == true) // True döndüğünde bu Tc ile kayıtlı kullanıcı var demektir.
            {
                ViewBag.TcErrorMessage = "Bu Tc ile kayitli kullanici bulunmaktadir.";
                ViewBag.ErrorRegisterMessage = "Lütfen alanları doğru bir şekilde doldurunuz.";
                return View("_UserRegister");
            }
            else if (CheckEmail(model) == true) // True döndüğünde bu E-mail ile kayıtlı kullanıcı var demektir.
            {
                ViewBag.EmailErrorMessage = "Bu E-posta ile kayitli kullanici bulunmaktadir.";
                ViewBag.ErrorMessage = "Lütfen alanları doğru bir şekilde doldurunuz.";
                return View("_UserRegister");
            }
            else // Bu durumda bu bilgilerle kayıtlı kullanıcı yoktur ve yeni kullanıcı kaydı yapılır.
            {
                dB.Users.Add(model);
                dB.SaveChanges();
                ViewBag.SuccesMessage = "Kullanıcı kayıt işlemi başarılı.";
                return View("_UserRegister");
            }
        }

        public bool CheckTcNo(User model) // Kayıt olucak kullanıcının Tc numarasının daha önceden olup olmadığını kontrol eder.
        {
            var ResultCheckTc = dB.Users.FirstOrDefault(x => x.TcNo == model.TcNo);
            // Eğer ki kullanıcı varsa kullanıcı bilgilerini yoksa null döndürecek.

            if (ResultCheckTc != null) // Benim zaten bu durumda bu Tc ile kayıtlı kullanıcım var demektir.
                return true;
            else // Bu durumda bu Tc ile kayıtlı kullanıcı yok demektir.
                return false;
        }

        public bool CheckEmail(User model) // Kayıt olucak kullanıcının E-postasının daha önceden olup olmadığını kontrol eder.
        {
            var ResultCheckEmail = dB.Users.FirstOrDefault(x => x.Email == model.Email);
            // Eğer ki kullanıcı varsa kullanıcı bilgilerini yoksa null döndürecek.

            if (ResultCheckEmail != null) // Benim zaten bu durumda bu e-posta ile kayıtlı kullanıcım var demektir.
                return true;
            else // Bu durumda bu e-posta ile kayıtlı kullanıcı yoktur.
                return false;
        }

        #endregion

        // Personel giriş kontrolleri burada yer alır.
        #region _EmployeeLogin

        [HttpGet] // _EmployeeLogin'e gelen istekleri bu action karşılıcak.
        public IActionResult _EmployeeLogin()
        {
            return View();
        }

        [HttpPost] // _EmployeeLogin'e formdan gelen verileri bu action karşılıcak.
        public IActionResult _EmployeeLogin(Employee model)
        {
            var employeecontrol = dB.Employees.FirstOrDefault(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password));
            // Kullanıcı varsa kullanıcı bilgilerini yoksa null döndürür.

            if (employeecontrol != null) // Bu durumda personel bilgileri doğru girilmiş demektir.
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else if (model.Email == null && model.Password == null) // Eğer ki e-posta ve şifre aynı anda boş girilirse sayfa tekrar yüklenir.
            {
                return View("_EmployeeLogin");
            }
            else // Bu durumda da personel bilgileri yanlış girilmiş demektir.
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
                return View("_EmployeeLogin");
            }
        }

        #endregion

        // Personel kayıt işlemleri burada yer alır.
        #region _EmployeeRegister

        [HttpGet] // _EmployeeRegister'e gelen istekleri bu action karşılıcak.
        public IActionResult _EmployeeRegister()
        {
            return View();
        }

        [HttpPost] // _EmployeeRegister'e gelen istekleri bu action karşılıcak.
        public IActionResult _EmployeeRegister(Employee model)
        {
            if (CheckTcNo(model) == true) // True döndüğünde bu Tc ile kayıtlı kullanıcı var demektir.
            {
                ViewBag.TcErrorMessage = "Bu Tc ile kayitli kullanici bulunmaktadir.";
                ViewBag.ErrorRegisterMessage = "Lütfen alanları doğru bir şekilde doldurunuz.";
                return View("_EmployeeRegister");
            }
            else if (CheckEmail(model) == true) // True döndüğünde bu E-mail ile kayıtlı kullanıcı var demektir.
            {
                ViewBag.EmailErrorMessage = "Bu E-posta ile kayitli kullanici bulunmaktadir.";
                ViewBag.ErrorRegisterMessage = "Lütfen alanları doğru bir şekilde doldurunuz.";
                return View("_EmployeeRegister");
            }
            else // Bu durumda bu bilgilerle kayıtlı kullanıcı yoktur ve yeni kullanıcı kaydı yapılır.
            {
                dB.Employees.Add(model);
                dB.SaveChanges();
                ViewBag.SuccessRegisterMessage = "Kullanıcı kayıt işlemi başarılı.";
                return View("_EmployeeRegister");
            }
        }

        public bool CheckTcNo(Employee model) // Kayıt olucak kullanıcının Tc numarasının daha önceden olup olmadığını kontrol eder.
        {
            var ResultCheckTc = dB.Employees.FirstOrDefault(x => x.TcNo == model.TcNo);
            // Eğer ki kullanıcı varsa kullanıcı bilgilerini yoksa null döndürecek.

            if (ResultCheckTc != null) // Benim zaten bu durumda bu Tc ile kayıtlı kullanıcım var demektir.
                return true;
            else // Bu durumda bu Tc ile kayıtlı kullanıcı yok demektir.
                return false;
        }

        public bool CheckEmail(Employee model) // Kayıt olucak kullanıcının E-postasının daha önceden olup olmadığını kontrol eder.
        {
            var ResultCheckEmail = dB.Employees.FirstOrDefault(x => x.Email == model.Email);
            // Eğer ki kullanıcı varsa kullanıcı bilgilerini yoksa null döndürecek.

            if (ResultCheckEmail != null) // Benim zaten bu durumda bu e-posta ile kayıtlı kullanıcım var demektir.
                return true;
            else // Bu durumda bu e-posta ile kayıtlı kullanıcı yoktur.
                return false;
        }

        #endregion

    }
}
