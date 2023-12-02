using System.ComponentModel.DataAnnotations;

namespace TaskBuddy.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public int? ChatId { get; set; }

        public string? Contenu { get; set; }

        public int? EmetteurId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
