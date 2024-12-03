using System.ComponentModel.DataAnnotations;

namespace odev2.Models
{
    public class Appointment
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string Name { get; set; } // Kullanıcı Adı

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // Randevu Tarihi

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; } // Randevu Saati
    }
}
