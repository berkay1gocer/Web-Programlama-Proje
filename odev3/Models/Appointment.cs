using System;
using System.ComponentModel.DataAnnotations;

namespace odev3.Models
{
    public class Appointment
    {
        public int Id { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string Kullanici { get; set; } // Kullanıcı Adı

        [Required]
        public int BarberId { get; set; } // Foreign Key

        public Barber? Barber { get; set; } // Navigation Property

        [Required]
        public string Islem { get; set; } // İşlem bilgisi

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // Randevu Tarihi

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; } // Randevu Saati
    }
}
