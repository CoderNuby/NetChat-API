using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<TypingNotification> TypingNotifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Message>().HasOne(x => x.Sender)
                .WithMany(x => x.Messages).HasForeignKey(x => x.SenderId);
            modelBuilder.Entity<Channel>().HasMany(x => x.TypingNotifications)
                .WithOne(x => x.Channel).HasForeignKey(x => x.ChannelId);
            modelBuilder.Entity<AppUser>().HasMany(x => x.TypingNotifications)
                .WithOne(x => x.Sender).HasForeignKey(x => x.SenderId);
        }
    }
}
