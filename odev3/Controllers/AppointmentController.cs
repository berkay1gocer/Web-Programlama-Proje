using Microsoft.AspNetCore.Mvc;
using odev3.Models;

namespace odev2.Controllers
{
    public class AppointmentController : Controller
    {
        private static List<Appointment> _appointments = new List<Appointment>(); // Geçici Liste (Database yoksa)

        // Randevu Formunu Göster
        public IActionResult Create()
        {
            return View();
        }

        // Randevu Alımı (POST)
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointments.Add(appointment); // Yeni randevuyu listeye ekle
                return RedirectToAction("List"); // Liste sayfasına yönlendir
            }
            return View(appointment); // Hatalıysa tekrar forma dön
        }

        // Randevu Listesini Göster
        public IActionResult List()
        {
            return View(_appointments); // Tüm randevuları listele
        }
    }
}
