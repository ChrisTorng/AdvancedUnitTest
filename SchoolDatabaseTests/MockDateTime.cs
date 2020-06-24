using System;

namespace SchoolDatabase.Tests
{
    internal class MockDateTime : IDateTime
    {
        public MockDateTime(DateTime now)
        {
            this.Now = now;
        }

        public DateTime Now { get; }
    }
}
