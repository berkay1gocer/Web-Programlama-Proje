using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using odev3.Models;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UyeOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UyeOl(User user)
        {
            return View();
        }

        public IActionResult GirisYap()
        {
            return View();
        }

        public IActionResult KullaniciSayfa()
        {
            return View();
        }

        // GET: RandevuAl
        [HttpGet]
        public IActionResult RandevuAl()
        {
            // Berber tablosundan verileri çekip SelectList'e dönüştür
            var berberList = _context.Barbers
                .Select(b => new SelectListItem
                {
                    Value = b.Ad, // Dropdown değeri (berber adı)
                    Text = $"{b.Ad} - {b.Uzmanlik}" // Görünen kısım: Ad ve Uzmanlık
                }).ToList();

            // ViewBag'e berber listesini ekle
            ViewBag.BerberList = berberList;

            return View();
        }

        // POST: RandevuAl
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuAl(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Appointment tablosuna yeni randevu kaydı ekle
                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();

                    // Kullanıcıya başarılı kayıt mesajı gönder
                    TempData["SuccessMessage"] = "Randevunuz başarıyla alındı!";
                    return RedirectToAction("RandevuAl");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Bir hata oluştu: {ex.Message}");
                }
            }

            // Eğer hata oluşursa, berber listesini tekrar yükle
            var berberList = _context.Barbers
                .Select(b => new SelectListItem
                {
                    Value = b.Ad,
                    Text = $"{b.Ad} - {b.Uzmanlik}"
                }).ToList();
            ViewBag.BerberList = berberList;

            return View(appointment);
        }

        public IActionResult RandevuYonet()
        {
            return View();
        }
    }
}
