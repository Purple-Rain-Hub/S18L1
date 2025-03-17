using Microsoft.EntityFrameworkCore;
using S18L1.Models;

namespace S18L1.Data
{
    public class S18L1DbContext : DbContext
    {
        public S18L1DbContext(DbContextOptions<S18L1DbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
