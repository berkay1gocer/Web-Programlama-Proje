using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using odev3.Models;
using odev3.Views;

namespace odev3.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Veritabanı tabloları
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Barber> Barbers { get; set; }

        public DbSet<Service> Services { get; set; }

        // Model ilişkileri
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Barber ve Appointment arasındaki ilişki
            modelBuilder.Entity<Barber>()
                .HasMany(b => b.Randevular) // Barber birden fazla Appointment'a sahip olabilir
                .WithOne(a => a.Barber) // Appointment bir Barber'a ait olabilir
                .HasForeignKey(a => a.BarberId) // BarberId, Foreign Key
                .OnDelete(DeleteBehavior.Cascade); // Barber silindiğinde ilişkili Appointment'lar silinir

            base.OnModelCreating(modelBuilder);
        }

        // Services tanımı
        public static void AddServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)); // SQL Server için bağlantı dizesi
        }
    }
}
