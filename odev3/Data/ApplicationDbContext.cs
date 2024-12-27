using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using odev3.Models;

namespace odev3.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}



		public DbSet<Service> Services { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Salon> Salons { get; set; }
	}



	// Identity için özel bir kullanıcı sınıfı (isteğe bağlı)
	public class ApplicationUser : IdentityUser
	{
		// İhtiyaç duyulursa burada özel özellikler ekleyebilirsiniz.
	}
}
