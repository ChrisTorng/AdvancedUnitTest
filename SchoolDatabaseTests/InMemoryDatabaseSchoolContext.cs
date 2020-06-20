using System;
using Microsoft.EntityFrameworkCore;

namespace SchoolDatabase.Tests
{
    internal class InMemoryDatabaseSchoolContext : BaseSchoolDatabase
    {
        public InMemoryDatabaseSchoolContext()
            : base(new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options)
        {
        }
    }
}
