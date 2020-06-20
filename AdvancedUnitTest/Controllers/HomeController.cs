using System.Diagnostics;
using System.Linq;
using AdvancedUnitTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolDatabase;

namespace AdvancedUnitTest.Controllers
{
    public class HomeController : Controller
    {
#pragma warning disable IDE0052 // Remove unread private members
        private readonly ILogger<HomeController> logger;
#pragma warning restore IDE0052 // Remove unread private members

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            using var schoolContent = new SchoolContext();
            var db = new StudentRepository(schoolContent);

            this.ViewData["NameSortParm"] =
                string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;

            this.ViewData["DateSortParm"] =
                sortOrder == "date" ? "date_desc" : "date";

            this.ViewData["SearchString"] = searchString;

            var students = db.CurrentStudents;

            if (!string.IsNullOrEmpty(searchString))
            {
#pragma warning disable CA1307 // Specify StringComparison
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
#pragma warning restore CA1307 // Specify StringComparison
            }

            students = sortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.LastName),
                "date" => students.OrderBy(s => s.EnrollmentDate),
                "date_desc" => students.OrderByDescending(s => s.EnrollmentDate),
                _ => students.OrderBy(s => s.LastName),
            };

            return this.View(students.AsNoTracking().ToList());
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
