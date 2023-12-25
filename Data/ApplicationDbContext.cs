using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Models;

namespace TaskBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext<Utilisateur>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages {  get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Tache> Tasks { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        public DbSet<Ami> Amis { get; set; }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Utilisateur>()
                .Ignore(e => e.Photo);
        }
        */


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>()
                .HasMany(u => u.Taches)
                .WithOne(t => t.Utilisateur)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade); // or .OnDelete(DeleteBehavior.Cascade) if you want to cascade delete

            modelBuilder.Entity<Utilisateur>()
                .HasMany(u => u.Amis)
                .WithOne(t => t.AmiUtilisateur)
                .HasForeignKey(t => t.UtilisateurId)
                .OnDelete(DeleteBehavior.Cascade); // or .OnDelete(DeleteBehavior.Cascade) if you want to cascade delete

            modelBuilder.Entity<Utilisateur>()
                .HasMany(u => u.Invitations)
                .WithOne(t => t.Destinataire)
                .HasForeignKey(t => t.DestinataireId)
                .OnDelete(DeleteBehavior.Cascade); // or .OnDelete(DeleteBehavior.Cascade) if you want to cascade delete
            /*
            modelBuilder.Entity<Utilisateur>()
                .HasMany(u => u.Invitations)
                .WithOne(t => t.Expediteur)
                .HasForeignKey(t => t.ExpediteurId)
                .OnDelete(DeleteBehavior.Cascade); // or .OnDelete(DeleteBehavior.Cascade) if you want to cascade delete
            */

            base.OnModelCreating(modelBuilder);
        }
    }
}