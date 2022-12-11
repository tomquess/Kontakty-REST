using KontaktyBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace KontaktyBackend.Data
{
    public class KontaktyDbContext : DbContext
    {
        public KontaktyDbContext(DbContextOptions<KontaktyDbContext> options) : base(options) { }

        public DbSet<KontaktModel> Kontakty { get; set; }
    }
}
