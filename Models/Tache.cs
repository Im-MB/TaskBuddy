using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBuddy.Models
{
    public class Tache
    {
        [Key]
        public int IdTask { get; set; }

        [Required]
        public string? Nom { get; set; }
        [Required]
        public string? Etat { get; set; }
        [Required]
        public DateTime DateD { get; set; }
        [Required]
        public DateTime DateF { get; set; }
        [Required]
        public string? Note { get; set; }
        [Required]
        public int? Reward { get; set; }

        [Required]
        public string? Priority { get; set; }


        public string? UserId { get; set; }

        [ForeignKey("UserId")]

          public Utilisateur Utilisateur { get; set; }





    }
}
