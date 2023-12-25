using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBuddy.Models
{
    public class Ami
    {
        public string? UtilisateurId { get; set; }
        public string? AmiId { get; set; }

        [ForeignKey("AmiId")]
        public Utilisateur? AmiUtilisateur { get; set; }
    }
}
