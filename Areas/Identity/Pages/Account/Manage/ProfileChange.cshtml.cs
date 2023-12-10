using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using NuGet.Versioning;
using TaskBuddy.Models;

namespace Test.Areas.Identity.Pages.Account.Manage
{
    public class ProfileChangeModel : PageModel
    {

        private readonly UserManager<Utilisateur> _userManager;
        private readonly SignInManager<Utilisateur> _signInManager;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProfileChangeModel(UserManager<Utilisateur> userManager,SignInManager<Utilisateur> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public string Username { get; set; }

        public string Email { get; set; }


        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        
        public class InputModel
        {
     
           
            [Display(Name = "Nom")]
            public string Nom { get; set; }

            [Display(Name = "Prenom")]
            public string Prenom { get; set; }

            [Display(Name = "Adresse")]
            public string Adresse { get; set; }

            [Display(Name = "Role")]
            public string Role { get; set; }

            [Display(Name = "Phone Number")]
            public string Tel { get; set; }

            [Display(Name = "Ville")]
            public string Ville { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[]? Profil { get; set; }

        }


        private async Task LoadAsync(Utilisateur user)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            var userTable = await _userManager.GetUserAsync(User);

            Username = userName;

            Input = new InputModel
            {

                Nom = userTable.Nom,

                Prenom = userTable.Prenom,

                Adresse = userTable.Adresse,

                Role = userTable.Role,

                Tel = userTable.Tel,

                Ville = userTable.Ville,

                Profil = user.Profil

            };
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            if (Input.Nom != user.Nom) {user.Nom = Input.Nom; }
            if (Input.Prenom != user.Prenom) { user.Prenom = Input.Prenom; }
            if (Input.Adresse != user.Adresse) { user.Adresse = Input.Adresse; }
            if (Input.Role != user.Role) { user.Role = Input.Role; }
            if (Input.Tel != user.Tel) { user.Tel = Input.Tel; }
            if (Input.Ville != user.Ville) { user.Ville = Input.Ville; }


           
            
            if (Request.Form.Files.Count > 0)
            {
                var file = Request.Form.Files.FirstOrDefault();

                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.Profil = dataStream.ToArray();
                }
            }
            
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has been updated";
                return RedirectToPage();
            }
            else
            {
                await _signInManager.RefreshSignInAsync(user);
                StatusMessage = "Your profile has NOT been updated";
                return RedirectToPage();

            }

            
        }
        
    }
}
