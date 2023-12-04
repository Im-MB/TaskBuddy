using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskBuddy.Models;

namespace TaskBuddy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Message {  get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}