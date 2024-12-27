using Microsoft.AspNetCore.Mvc;

namespace odev3.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public decimal Price { get; set; }
        public string ExpertiseRequired { get; set; } // Example: "Haircut", "Shave"
    }

}
