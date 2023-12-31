using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly ApplicationDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger,
           ApplicationDbContext dbcontext)
        {
            _logger = logger;

            _dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> TasksDoneCount()
        {
            // Get the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Count the number of tasks marked as 'done' for the current user
            var tasksDoneCount = await _dbcontext.Tasks
                .Where(t => t.UserId == userId && t.Etat == "done")
                .CountAsync();

            return Content($"{tasksDoneCount}");
        }

        public async Task<IActionResult> TasksInProgressCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var tasksInProgressCount = await _dbcontext.Tasks
                .Where(t => t.UserId == userId && t.Etat == "in progress")
                .CountAsync();

            return Content(tasksInProgressCount.ToString());
        }

        public async Task<IActionResult> TotalTasksCount()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var totalTasksCount = await _dbcontext.Tasks
                .Where(t => t.UserId == userId)
                .CountAsync();

            return Content(totalTasksCount.ToString());
        }


    }
}