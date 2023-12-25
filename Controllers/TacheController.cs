using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
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

        public IActionResult search(string nameSearch)
        {
            return View(_dbContext.Tasks.Where(x => x.Nom.Contains(nameSearch) || nameSearch == null).ToList());
        }

        [HttpGet]
        public async Task<IActionResult> ListeTaches()
        {
            var currentUser = await _userManager.GetUserAsync(User);

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
                if (tache.Priority == "High") tache.Reward = 3;
               else if (tache.Priority == "Medium") tache.Reward = 2;
               else if (tache.Priority == "Low") tache.Reward = 1;
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
            if (tache != null)
            {
                _dbContext.Tasks.Remove(tache);
                _dbContext.SaveChanges();

            }
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
            oldTache.DateD = newTache.DateD;
            oldTache.DateF=newTache.DateF;
            oldTache.Note = newTache.Note;
             _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(ListeTaches));
        }

        [HttpPost]
        [Route("Tache/UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateTaskStatus(int id)
        { var user = await _userManager.GetUserAsync(User);
            var task = _dbContext.Tasks.Find(id);

            if (task != null && user != null)
            {
                task.Etat = task.Etat == "in progress" ? "done" : "in progress";
                _dbContext.Update(task);
                if (task.Etat=="done") user.MyScore += task.Reward;
                else if (task.Etat=="in progress" && user.MyScore!=0) user.MyScore -= task.Reward;
                await _userManager.UpdateAsync(user);
                _dbContext.SaveChanges();
                return Ok();
            }

            return NotFound();
        }


    }
}
