using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using odev3.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace odev3.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor: ApplicationDbContext enjekte edilir
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Ana sayfa
        public IActionResult Index()
        {
            return View();
        }

        // Üye Ol Sayfası GET
        public IActionResult UyeOl()
        {
            return View();
        }

        // Üye Ol POST
        [HttpPost]
        public async Task<IActionResult> UyeOl(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Yeni kullanıcı kaydı ekle
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    // Kullanıcıya başarılı kayıt mesajı göster
                    TempData["SuccessMessage"] = "Üyelik başarılı bir şekilde oluşturuldu!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                }
            }

            return View(user);
        }

        // Giriş Yap Sayfası GET
        public IActionResult GirisYap()
        {
            return View();
        }

        // Giriş Yap POST
        [HttpPost]
        public async Task<IActionResult> GirisYap(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Başarılı giriş
                TempData["SuccessMessage"] = "Giriş başarılı!";


                return RedirectToAction("KullaniciSayfa");
            }

            // Hatalı giriş
            ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            return View();
        }

        // Kullanıcı Sayfası
        public IActionResult KullaniciSayfa()
        {
            return View();
        }

        // Randevu Al Sayfası GET
        [HttpGet]
        public IActionResult RandevuAl()
        {
            var berberList = _context.Barbers
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(), // ID değerini burada kullanıyoruz
                    Text = $"{b.Ad} - {b.Uzmanlik}" // Görünen kısım: Ad ve Uzmanlık
                }).ToList();

            ViewBag.BerberList = berberList;
            return View();
        }

        // Randevu Al POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuAl(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Randevunuz başarıyla alındı!";
                    return RedirectToAction("RandevuAl");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                }
            }

            var berberList = _context.Barbers
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Ad} - {b.Uzmanlik}"
                }).ToList();
            ViewBag.BerberList = berberList;

            return View(appointment);
        }

        // Randevu Yönetimi
        public IActionResult RandevuYonet()
        {
            var randevular = _context.Appointments.ToList();
            return View(randevular);
        }
    }
}
