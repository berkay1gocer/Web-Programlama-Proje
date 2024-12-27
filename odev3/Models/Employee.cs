using Microsoft.AspNetCore.Mvc;

namespace odev3.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Expertise { get; set; } // Example: "Haircut", "Shave"
        public bool IsAvailable { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }

}
