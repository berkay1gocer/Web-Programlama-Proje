using Microsoft.EntityFrameworkCore;
using odev3.Models;

namespace odev3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

    }
}
