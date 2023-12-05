using System.ComponentModel.DataAnnotations;

namespace TaskBuddy.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        public string? Contenu { get; set; }
    }
}
