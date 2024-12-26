using Microsoft.AspNetCore.Mvc;
using odev3.Models;
using System.Linq;
using System.Threading.Tasks;

namespace odev3.Controllers
{
    public class BarberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarberController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Berberin randevularını listeleme sayfası
        [HttpGet]
        public IActionResult Randevularim(int barberId)
        {
            // BarberId ile eşleşen randevuları filtrele
            var randevular = _context.Appointments
                .Where(a => a.BarberId == barberId)
                .Select(a => new AppointmentViewModel
                {
                    Kullanici = a.Kullanici,
                    Islem = a.Islem,
                    Date = a.Date,
                    Time = a.Time
                }).ToList();

            ViewBag.BarberId = barberId; // Berber ID'sini sakla
            return View(randevular);
        }

        // POST: Yeni bir randevu ekleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RandevuEkle(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Appointments.Add(appointment);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Randevu başarıyla eklendi!";
                    return RedirectToAction("Randevularim", new { barberId = appointment.BarberId });
                }
                catch
                {
                    ModelState.AddModelError("", "Randevu eklenirken bir hata oluştu.");
                }
            }

            return View(appointment);
        }
    }

    // ViewModel: Appointment için sadeleştirilmiş veri modeli
    public class AppointmentViewModel
    {
        public string Kullanici { get; set; }
        public string Islem { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
    }
}
