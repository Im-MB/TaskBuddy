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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TaskBuddy.Controllers
{
    [Authorize]
    public class UtilisateurController : Controller
    {

        private readonly UserManager<Utilisateur> userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<Utilisateur> _signInManager;
        private readonly IUserStore<Utilisateur> _userStore;
        private readonly IUserEmailStore<Utilisateur> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        


        public UtilisateurController(UserManager<Utilisateur> userManager,
            IUserStore<Utilisateur> userStore,
            SignInManager<Utilisateur> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            _userStore = userStore;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _webHostEnvironment = webHostEnvironment;
        }


        //--------------------------------------


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
                    return RedirectToAction("ListeUtilisateur");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListeUtilisateur");
            }
        }

        [HttpGet]

        public IActionResult search(string nameSearch)
        {
            return View(userManager.Users.Where(x => x.UserName.Contains(nameSearch) || nameSearch == null).ToList());
        }
        [HttpGet]
        public async Task<IActionResult> UserDetails(string id)
        {  var user= await userManager.FindByIdAsync(id);

            return View(user);
        }       

        [HttpGet]
        public async Task<IActionResult> EditUtilisateur(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> EditUtilisateur(Utilisateur newUser)
        {
            var oldUser = await userManager.FindByIdAsync(newUser.Id);
            if (oldUser != null)
            {

                oldUser.Role = newUser.Role;

                var result = await userManager.UpdateAsync(oldUser);

                /*
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(oldUser);
                    return RedirectToAction(nameof(ListeUtilisateur));
                }
                else
                {
                    await _signInManager.RefreshSignInAsync(oldUser);
                    return RedirectToAction(nameof(ListeUtilisateur));

                }
                */
            }
            return RedirectToAction(nameof(ListeUtilisateur));
        }
    }
}
