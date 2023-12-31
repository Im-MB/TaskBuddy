
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskBuddy.Models;
namespace TaskBuddy.Models
{
    public class TachePartagee
    {
        public int Id    { get; set; }

        public int TacheId {  get; set; }

        public Tache? Tache { get; set; }

        public string?  ExpediteurId { get; set; }

        public Utilisateur? Expediteur { get; set; }

        public string? DestinataireId { get; set; }

        public Utilisateur? Destinataire { get; set; }   
    }
}
