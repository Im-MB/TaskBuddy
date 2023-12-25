namespace TaskBuddy.Models
{

    public class Invitation
    {
        public int Id { get; set; }
        public string? ExpediteurId { get; set; }
        public string? DestinataireId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDeclined { get; set; }
        public Utilisateur? Destinataire { get; set; }

        public Utilisateur? Expediteur { get; set; }
    }
}
