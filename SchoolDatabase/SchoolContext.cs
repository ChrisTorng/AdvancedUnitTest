using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase
{
    public class SchoolContext : DbContext, ISchoolDatabase
    {
        public SchoolContext()
            : base(new DbContextOptionsBuilder<SchoolContext>()
                .UseSqlite("Data Source=SchoolContext.db")
                .Options)
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        IQueryable<Student> ISchoolDatabase.Students =>
            this.Students;
    }
}
