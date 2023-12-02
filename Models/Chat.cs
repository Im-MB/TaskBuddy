using System.ComponentModel.DataAnnotations;

namespace TaskBuddy.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }

        public int EmetteurId { get; set; }

        public int RecepteurId { get; set; }

    }
}
