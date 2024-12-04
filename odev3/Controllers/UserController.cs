using Microsoft.AspNetCore.Mvc;
using odev3.Models;

namespace odev3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UyeOl()
        {
            return View();
        }

        public async Task<IActionResult> UyeOl(User user)
        {
            return View();
        }

        public IActionResult GirisYap()
        {
            return View();
        }
    }
}
