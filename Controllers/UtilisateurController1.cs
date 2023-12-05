using Microsoft.AspNetCore.Mvc;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Controllers
{
    public class UtilisateurController1 : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public UtilisateurController1(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet]
        public IActionResult listeuser()
        {
            var listeuser = _dbcontext.Utilisateur.ToList();

            return View(listeuser);
        }

        [HttpGet]
        public IActionResult crerUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult crerUser(Utilisateur stagire)
        {
            _dbcontext.Utilisateur.Add(stagire);
            _dbcontext.SaveChanges();
            return RedirectToAction(nameof(listeuser));
        }

        [HttpGet]
        public IActionResult afficher(int id)
        {
            var user = _dbcontext.Utilisateur.SingleOrDefault(i => i.IdUtilisateur == id);
            return View(user);
        }

        [HttpGet]
        public IActionResult supprimer(int id)
        {
            var user = _dbcontext.Utilisateur.Find(id);
            return View(user);
        }
        [HttpPost]
        public IActionResult supprimer(Utilisateur user)
        {
            _dbcontext.Utilisateur.Remove(user);
            _dbcontext.SaveChanges();
            return RedirectToAction(nameof(listeuser));
        }

        [HttpGet]
        public IActionResult modifier(int id)
        {
            var user = _dbcontext.Utilisateur.SingleOrDefault(i => i.IdUtilisateur == id);
            return View(user);
        }
        [HttpPost]
        public IActionResult modifier(Utilisateur newUser)
        {
            var oldUser = _dbcontext.Utilisateur.Find(newUser.IdUtilisateur);
            if (oldUser != null)
            {
                if (oldUser.Type == newUser.Type)
                {
                    oldUser.Nom = newUser.Nom;
                    oldUser.Prenom = newUser.Prenom;
                    oldUser.Adresse = newUser.Adresse;
                    oldUser.Tel = newUser.Tel;
                    oldUser.Photo = newUser.Photo;
                    oldUser.Ville = newUser.Ville;
                    oldUser.Score = newUser.Score;
                    _dbcontext.SaveChanges();
                }
            }

            return RedirectToAction(nameof(listeuser));
        }
    }
}
