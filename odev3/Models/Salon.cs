using Microsoft.AspNetCore.Mvc;

namespace odev3.Models
{
    public class Salon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<Service> Services { get; set; }
    }

}
