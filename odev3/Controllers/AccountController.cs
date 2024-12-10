using Microsoft.AspNetCore.Mvc;
using odev3.Models;

namespace odev3.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;
        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

		// Giriş Sayfası
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(User us)
		{
			if (ModelState.IsValid)
			{
				// Kullanıcıyı veritabanında arıyoruz
				var user = _context.Users.FirstOrDefault(u => u.Username == us.Username);

				if (user != null && user.Password == us.Password)
				{
					// Giriş başarılı, kullanıcıyı oturum açmış olarak işaretleyelim (örneğin Session kullanarak)
					//HttpContext.Session.SetString("Username", user.Username);
					return RedirectToAction("KullaniciSayfa", "User");  // Giriş başarılı, ana sayfaya yönlendir
				}
				else
				{
					// Kullanıcı adı veya şifre hatalı olduğunda hata mesajı ekle
					ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
				}
			}

			// ModelState geçerli değilse veya hata varsa, Login formunu yeniden göster
			return View(us);
		}
		// Üye Ol Sayfası
		[HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Şifreyi hash'lemek istiyorsanız burada yapabilirsiniz (ör. BCrypt veya ASP.NET Identity).

                // Kullanıcıyı veritabanına ekle
                _context.Users.Add(user);
                _context.SaveChanges();

                // Başarılı kayıt sonrası yönlendirme
                return RedirectToAction("Login", "Account");
            }

            else
            {
                return View(user);
            }
            
        }
    }
}