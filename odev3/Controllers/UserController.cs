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
        [HttpGet]
        public IActionResult RandevuAl()
        {
            // Berber ve işlem listelerini View'e gönderiyoruz
            var berberler = _context.Barbers.ToList();
            var islemler = _context.Services.ToList();

            ViewBag.Berberler = berberler;
            ViewBag.Islemler = islemler;

            return View();
        }

        // POST: RandevuAl
        [HttpPost]
        public IActionResult RandevuAl(int kullaniciId, int barberId, int islemId, DateTime tarihSaat)
        {
            // Tarih ve saat kontrolü yapılabilir
            if (tarihSaat < DateTime.Now)
            {
                ModelState.AddModelError("tarihSaat", "Geçmiş bir tarih seçemezsiniz.");
                return RedirectToAction("RandevuAl");
            }

            // Aynı tarih ve saat için çakışma kontrolü
            bool randevuVarMi = _context.Appointments.Any(a => a.BarberId == barberId && a.DateTime == tarihSaat);

            if (randevuVarMi)
            {
                ModelState.AddModelError("tarihSaat", "Bu tarih ve saat için berber zaten dolu.");
                return RedirectToAction("RandevuAl");
            }

            // Randevu kaydı oluştur
            var yeniRandevu = new Appointment
            {
                Kullanici = kullanici,
                BarberId = barberId,
                Islem = islem,
                DateTime = tarihSaat
            };

            _context.Appointments.Add(yeniRandevu);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Randevu başarıyla alındı!";
            return RedirectToAction("Randevularim");
        }



    }
}
