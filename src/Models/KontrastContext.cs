using Microsoft.EntityFrameworkCore;

namespace KontrastDB.Models
{
    public class KontrastContext : DbContext
    {
        public KontrastContext(DbContextOptions<KontrastContext> options) : base(options) { }

        public DbSet<Users> Users => Set<Users>();
    }
}
