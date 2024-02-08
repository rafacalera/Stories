using Microsoft.EntityFrameworkCore;
using Stories.API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stories.API.Data
{
    public class StoriesContext : DbContext
    {

        public StoriesContext(DbContextOptions<StoriesContext> options) : base(options) { }
        public DbSet<Story> Story { get; set; }
        public DbSet<Vote> Vote { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this.Database.GetConnectionString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Story>().ToTable("Stories");
            modelBuilder.Entity<Story>()
                .HasMany(e => e.Votes)
                .WithOne(e => e.Story)
                .HasForeignKey(e => e.StoryId)
                .IsRequired();
            modelBuilder.Entity<Story>().HasKey(e => e.Id);
            modelBuilder.Entity<Story>().Property(e => e.Title).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Story>().Property(e => e.Description).HasMaxLength(250).IsRequired();
            modelBuilder.Entity<Story>().Property(e => e.Departament).HasMaxLength(50).IsRequired();
            
            modelBuilder.Entity<Vote>().ToTable("Votes");
            modelBuilder.Entity<Vote>().HasKey(e => e.Id);
            modelBuilder.Entity<Vote>().Property(e => e.UpVote).IsRequired();
            
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>()
                .HasMany(e => e.Votes)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            modelBuilder.Entity<User>().HasKey(e => e.Id);
            modelBuilder.Entity<User>().Property(e => e.Name).HasMaxLength(100).IsRequired();
        }
    }
}
