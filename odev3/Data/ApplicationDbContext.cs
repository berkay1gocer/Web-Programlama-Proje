using Microsoft.EntityFrameworkCore;
using odev3.Models;

namespace odev3.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
