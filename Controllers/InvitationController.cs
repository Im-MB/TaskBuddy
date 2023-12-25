using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Data;
using TaskBuddy.Models;


namespace TaskBuddy.Controllers
{
    public class InvitationController : Controller
    {
        private readonly UserManager<Utilisateur> userManager;

        private readonly ApplicationDbContext _dbcontext;


        public InvitationController(UserManager<Utilisateur> userManager,
          ApplicationDbContext dbcontext
           )
        {
            this.userManager = userManager;

            _dbcontext = dbcontext;



        }

        [HttpGet]

        public IActionResult search(string nameSearch)
        {
            return View(_dbcontext.Invitations.Where(x => x.DestinataireId.Contains(nameSearch) || nameSearch == null).ToList());
        }


        [HttpPost]
        public IActionResult invitations(string Id)
        {
            var expediteurId = userManager.GetUserId(User); // Obtenez l'ID de l'utilisateur connecté comme expéditeur

            var invitation = new Invitation
            {
                ExpediteurId = expediteurId,
                DestinataireId = Id,
                IsAccepted = false
                // Vous pouvez ajouter d'autres propriétés d'invitation ici
            };

            _dbcontext.Invitations.Add(invitation);
            _dbcontext.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult AccepterInvitation(int invitationId)
        {
            var invitation = _dbcontext.Invitations.Find(invitationId);

            if (invitation != null && !invitation.IsAccepted && !invitation.IsDeclined)
            {
                invitation.IsAccepted = true;

                var destinataireId = invitation.DestinataireId;
                var expediteurId = invitation.ExpediteurId;

                // Ajoutez l'ami dans la table Ami
                _dbcontext.Amis.Add(new Ami { UtilisateurId = destinataireId, AmiId = expediteurId });

                _dbcontext.SaveChanges();
            }

            return Ok(); // Ou une autre réponse appropriée
        }

        private void AjouterAmi(string utilisateurId, string amiId)
        {
            // Logique pour ajouter l'ami (vous devrez ajuster cela en fonction de votre modèle)
            // Exemple : _dbcontext.Amis.Add(new Ami { UtilisateurId = utilisateurId, AmiId = amiId });
        }

        [HttpGet]
        public IActionResult InvitationEnvoye()
        {
            var userId = userManager.GetUserId(User);

            var invitationsEnvoyees = _dbcontext.Invitations
                .Include(i => i.Destinataire)  // Inclure l'utilisateur destinataire
                .Where(i => i.ExpediteurId == userId)
                .ToList();

            return View(invitationsEnvoyees);
        }

        [HttpGet]
        public IActionResult AfficherInvitations()
        {
            var userId = userManager.GetUserId(User);

            var invitations = _dbcontext.Invitations
                .Include(i => i.Expediteur)  // Inclure l'utilisateur destinataire
                .Where(i => i.DestinataireId == userId && !i.IsAccepted)
                .ToList();

            return View(invitations);
        }

    }
}
