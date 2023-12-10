using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskBuddy.Data;
using TaskBuddy.Models;
using System.Net;
using Test.Areas.Identity.Pages.Account.Manage;
using System.ComponentModel.DataAnnotations;
using TaskBuddy.Areas.Identity.Pages.Account;

namespace TaskBuddy.Controllers
{
    [Authorize]
    public class UtilisateurController : Controller
    {

        private readonly UserManager<Utilisateur> userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UtilisateurController(UserManager<Utilisateur> userManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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

        
    }
}
