﻿using Microsoft.AspNetCore.Identity;
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

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Utilisateur>()
                .Ignore(e => e.Photo);
        }
        */
    }
}