using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskBuddy.Data;
using TaskBuddy.Models;

namespace TaskBuddy.Controllers
{
    public class TachePartageeController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;

        private readonly ApplicationDbContext _dbcontext;


        public TachePartageeController(UserManager<Utilisateur> userManager,
           ApplicationDbContext dbcontext
           )
        {
            this.userManager = userManager;

            _dbcontext = dbcontext;



        }

        [HttpGet]
        public IActionResult listeAmis()
        {
            //var listeAmis = _dbcontext.TachesPartagees.ToList();

            var viewModel = _dbcontext.TachesPartagees
        .Include(tp => tp.Tache)
        .Include(tp => tp.Expediteur)
        .Include(tp => tp.Destinataire)
        .ToList();

            var listeAmis = viewModel.Select(tp => new TachePartagee
            {
                Tache = tp.Tache,
                Expediteur = tp.Expediteur,
                Destinataire = tp.Destinataire
            }).ToList();

            return View(listeAmis);


        }
        
    }
}