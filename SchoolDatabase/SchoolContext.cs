using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase
{
    public class SchoolContext : DbContext, ISchoolDatabase
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

        IQueryable<Student> ISchoolDatabase.Students =>
            this.Students;
    }
}
