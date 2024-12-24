using Microsoft.EntityFrameworkCore;
using odev3.Models;
using odev3.Views;

namespace odev3.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }
        public DbSet<Randevu> Islem { get; set; }

    }
}
