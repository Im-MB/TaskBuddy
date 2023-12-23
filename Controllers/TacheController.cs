using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Controllers
{
    [Authorize]
    public class TacheController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<Utilisateur> _userManager;

        public TacheController(ApplicationDbContext dbContext, UserManager<Utilisateur> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

       
        [HttpGet]
        public async Task<IActionResult> ListeTaches()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Retrieve tasks associated with the current user
            var userTasks = _dbContext.Tasks.Where(task => task.UserId == currentUser.Id).ToList();

            return View(userTasks);
        }

        [HttpGet]
        public IActionResult CreateTache() {
            return View();
                
         }
        [HttpPost]
        public async Task<IActionResult> CreateTache(Tache tache)
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                tache.UserId = userId;
                tache.Etat = "in progress";
                tache.Priority = "High";
                tache.Reward = 0;
                tache.DateD = DateTime.Now;
                _dbContext.Tasks.Add(tache);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(ListeTaches)) ;
        }
        [HttpGet]
        public IActionResult Details(int id) {
        
        var tache=_dbContext.Tasks.SingleOrDefault (t => t.IdTask == id);
            return View(tache);
        
        }
        
        [HttpGet]
        [HttpPost]
        public IActionResult DeleteTache(int id)
        {
            var tache = _dbContext.Tasks.SingleOrDefault(t => t.IdTask == id);
            _dbContext.Tasks.Remove(tache);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(ListeTaches));
        }
       
        [HttpGet]
        public IActionResult EditTache(int id)
        {
            var tache= _dbContext.Tasks.Find(id);
            return View(tache);
        }
        [HttpPost]  
        public IActionResult EditTache(Tache newTache)
        {
            var oldTache= _dbContext.Tasks.Find(newTache.IdTask);
            if(oldTache != null) { 
            
            oldTache.Nom=newTache.Nom;
            oldTache.Etat = newTache.Etat;
            oldTache.DateD = newTache.DateD;
            oldTache.DateF=newTache.DateF;
            oldTache.Note = newTache.Note;
             _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(ListeTaches));
        }

        [HttpPost]
        [Route("Tache/UpdateStatus/{id}")]
        public IActionResult UpdateTaskStatus(int id)
        {
            var task = _dbContext.Tasks.Find(id);

            if (task != null)
            {
                task.Etat = task.Etat == "in progress" ? "done" : "in progress";
                _dbContext.Update(task);
                _dbContext.SaveChanges();
                return Ok();
            }

            return NotFound();
        }


    }
}
