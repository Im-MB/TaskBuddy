using System.ComponentModel.DataAnnotations;

namespace TaskBuddy.Models
{
    public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }
        [Required]
        public string? Type { get; set; }
        [Required]
        public string? Nom { get; set; }
        [Required]
        public string? Prenom { get; set; }
        [Required]

        public string? Adresse { get; set; }
        [Required]
        public int? Tel { get; set; }
        [Required]
        public string? Ville { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Passwd { get; set; }
        [Required]
        public int? Score { get; set; }
        [Required]
        public string? Photo { get; set; }
    }
}
