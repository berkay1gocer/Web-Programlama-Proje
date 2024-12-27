using Microsoft.AspNetCore.Mvc;

namespace odev3.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
