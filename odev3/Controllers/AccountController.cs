using Microsoft.AspNetCore.Mvc;
using odev3.Models;

namespace odev3.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> _users = new List<User> // Geçici kullanıcı listesi
        {
            new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "berber", Password = "berber123", Role = "Berber" }
        };

        // Giriş Sayfası
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            return View();

        }

        // Üye Ol Sayfası
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            return RedirectToAction("Login");
        }
    }
}