using System.ComponentModel.DataAnnotations;

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
    }
}
