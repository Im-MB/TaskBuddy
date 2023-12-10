using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBuddy.Models
{
    public class Utilisateur : IdentityUser
    {
        public string? Nom { get; set; }

        public string? Prenom { get; set; }

        public string? Adresse { get; set; }

        public string? Role { get; set; } 

        public string? Tel { get; set; }
        
        public string? Ville { get; set; }
        
        public string? MyScore { get; set; }
        
        public byte[]? Profil { get; set; }

        

    }
}
