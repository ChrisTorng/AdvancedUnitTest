using AdvancedUnitTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AdvancedUnitTest.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=SchoolContext.db");

        public DbSet<Student> Students { get; set; }
    }
}
