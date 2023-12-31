﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBuddy.Models
{
    public class Utilisateur : IdentityUser
    {
        public string? Nom { get; set; }

        public string? Prenom { get; set; }

        public string? Adresse { get; set; }

        public string? Role { get; set; } = "normalUser";

        public string? Tel { get; set; }

        public string? Ville { get; set; }

        public int? MyScore { get; set; } = 0;

        public byte[]? Profil { get; set; }

        public ICollection<Tache> Taches { get; set; }

        public ICollection<Ami> Amis { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

        //---------------------------------



    }
}
