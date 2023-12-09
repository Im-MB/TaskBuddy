using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Controllers
{
    [Authorize]
    public class UtilisateurController : Controller
    {

        private readonly UserManager<Utilisateur> userManager;


        public UtilisateurController(UserManager<Utilisateur> userManager)
        {
            this.userManager = userManager;
        }


        [HttpGet]
        public IActionResult ListeUtilisateur()
        {
            var users = userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUtilisateur");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUtilisateur");
            }
        }

        /*
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
         public IActionResult  crerUser(Utilisateur stagire)
         {

             _dbcontext.Utilisateur.Add(stagire);
             _dbcontext.SaveChanges();
             return RedirectToAction(nameof(listeuser));


         }

         [HttpGet]
         public IActionResult afficher(string id)
         {
             var user = _dbcontext.Utilisateur.SingleOrDefault(i => i.IdUsers == id);
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
        */
    }
}
