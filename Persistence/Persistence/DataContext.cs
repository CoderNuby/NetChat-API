using Domain;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Channel>().HasData(
                new Channel
                {
                    Id = Guid.NewGuid(),
                    Name = "DotNetCore",
                    Description = "This channel is dedicated to DotNet Core"
                },
                new Channel
                {
                    Id = Guid.NewGuid(),
                    Name = "Angular",
                    Description = "This channel is dedicated to Angular"
                },
                new Channel
                {
                    Id = Guid.NewGuid(),
                    Name = "ReactJs",
                    Description = "This channel is dedicated to ReactJs"
                }
            );
        }
    }
}
