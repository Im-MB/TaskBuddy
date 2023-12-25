using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBuddy.Data;
using TaskBuddy.Models;


namespace TaskBuddy.Controllers
{
    public class AmiController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;

        private readonly ApplicationDbContext _dbcontext;


        public AmiController(UserManager<Utilisateur> userManager,
           ApplicationDbContext dbcontext
           )
        {
            this.userManager = userManager;

            _dbcontext = dbcontext;



        }
        [HttpGet]

        public IActionResult search(string nameSearch)
        {
            return View(userManager.Users.Where(x => x.UserName.Contains(nameSearch) || nameSearch == null).ToList());
        }

        [HttpGet]
        public IActionResult listeAmis()
        {
            // Obtenez l'ID de l'utilisateur connecté (assurez-vous que votre système d'authentification est configuré)
            var utilisateurId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Récupérez la liste des amis de l'utilisateur connecté depuis la base de données
            var listeAmis = _dbcontext.Amis
                .Include(a => a.AmiUtilisateur) // Inclure l'utilisateur ami
                .Where(a => a.UtilisateurId == utilisateurId)
                .ToList();

            // Passez la liste des amis à la vue
            return View(listeAmis);
        }


    }
}
