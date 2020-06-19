using System.Diagnostics;
using System.Linq;
using AdvancedUnitTest.Data;
using AdvancedUnitTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public IActionResult Index(string sortOrder)
        {
            using var db = new SchoolContext();

            this.ViewData["NameSortParm"] =
                string.IsNullOrEmpty(sortOrder) ? "name_desc" : string.Empty;

            this.ViewData["DateSortParm"] =
                sortOrder == "Date" ? "date_desc" : "date";

            var students = db.Students.AsQueryable();

            switch (sortOrder)
            {
            case "name_desc":
                students = students.OrderByDescending(s => s.LastName);
                break;
            case "date":
                students = students.OrderBy(s => s.EnrollmentDate);
                break;
            case "date_desc":
                students = students.OrderByDescending(s => s.EnrollmentDate);
                break;
            default:
                students = students.OrderBy(s => s.LastName);
                break;
            }

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
