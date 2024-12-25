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
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
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
                    Value = b.Id.ToString(), // Barber ID
                    Text = $"{b.Ad}" // Berber Adı
                }).ToList();

            ViewBag.BerberList = berberList;
            return View();
        }

        // Randevu Al POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RandevuAl(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Appointments.Add(appointment);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Randevunuz başarıyla alındı!";
                    return RedirectToAction("KullaniciSayfa");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                }
            }

            // Eğer işlem başarısızsa, berber listesi tekrar yüklenir
            var berberList = _context.Barbers
                .Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = $"{b.Ad}"
                }).ToList();

            ViewBag.BerberList = berberList;
            return View(appointment);
        }

        // Ajax çağrısı: Uzmanlık Listesi
        [HttpGet]
        public JsonResult GetUzmanlik(int barberId)
        {
            var uzmanliklar = _context.Barbers
                .Where(b => b.Id == barberId)
                .Select(b => b.Uzmanlik)
                .ToList();

            return Json(uzmanliklar);
        }

        // Randevu Yönetimi
        public IActionResult RandevuYonet()
        {
            var randevular = _context.Appointments.Include(a => a.Barber).ToList();
            return View(randevular);
        }
    }
}
