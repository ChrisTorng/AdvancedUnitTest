using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SchoolDatabase.Tests
{
    internal class InMemorySqliteSchoolContext : BaseSchoolDatabase
    {
        private readonly DbConnection connection;
        private bool disposed = false;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public InMemorySqliteSchoolContext()
            : base(new DbContextOptionsBuilder<SchoolContext>()
#pragma warning disable CA2000 // Dispose objects before losing scope
                .UseSqlite(CreateInMemoryDatabase())
#pragma warning restore CA2000 // Dispose objects before losing scope
                .Options)
        {
            this.connection = RelationalOptionsExtension.Extract(this.ContextOptions).Connection;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.connection.Dispose();
            }

            this.disposed = true;

            base.Dispose(disposing);
        }
    }
}
