using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TimeLoggingApp.Data;
using TimeLoggingApp.Models;

namespace TimeLoggingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ApplicationDbContext context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Generate()
        {
            //this.context.Database.Migrate();

            for (int i = 0; i < 100; i++)
            {
                this.context.TeamMembers.Add(new TeamMember()
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    Email = $"{Guid.NewGuid()}@local.com"
                });
            }

            this.context.SaveChanges();

            return this.Redirect("~/");
        }


        public IActionResult Data()
        {
            var q = from t in context.TeamMembers
                    select t;
            var results = q.ToArray();
            //var results = context.TeamMembers.Where(t => t.FirstName == "Khurram")

            return this.View(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
