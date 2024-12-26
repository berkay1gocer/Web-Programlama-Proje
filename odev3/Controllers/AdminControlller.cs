using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using odev3.Models;
using System.Linq;

namespace odev3.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Admin Paneli Anasayfası
        public IActionResult Index()
        {
            return View();
        }

        // Kullanıcı Listesi
        public IActionResult KullaniciListesi()
        {
            var kullanicilar = _context.Users.ToList();
            return View(kullanicilar);
        }

        // Berber Listesi
        public IActionResult BerberListesi()
        {
            var berberler = _context.Barbers.ToList();
            return View(berberler);
        }

        // Yeni Berber Ekle (GET)
        public IActionResult BerberEkle()
        {
            return View();
        }

        // Yeni Berber Ekle (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BerberEkle(Barber barber)
        {
            if (ModelState.IsValid)
            {
                _context.Barbers.Add(barber);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Berber başarıyla eklendi.";
                return RedirectToAction("BerberListesi");
            }

            return View(barber);
        }

        // Randevu Listesi
        public IActionResult RandevuListesi()
        {
            var randevular = _context.Appointments
                .Select(r => new
                {
                    r.Id,
                    r.Date,
                    r.Time,
                    BarberName = _context.Barbers.FirstOrDefault(b => b.Id == r.BarberId).Ad,
                    r.BarberId
                })
                .ToList();
            return View(randevular);
        }

        // Randevu Sil
        public IActionResult RandevuSil(int id)
        {
            var randevu = _context.Appointments.Find(id);
            if (randevu != null)
            {
                _context.Appointments.Remove(randevu);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Randevu başarıyla silindi.";
            }

            return RedirectToAction("RandevuListesi");
        }
    }
}
