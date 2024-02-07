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
                .HasOne(e => e.Poll).WithOne(e => e.Story).HasForeignKey<Poll>(e => e.StoryId).IsRequired();
            modelBuilder.Entity<Story>().HasKey(e => e.Id);
            modelBuilder.Entity<Story>().Property(e => e.Title).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Story>().Property(e => e.Description).HasMaxLength(250).IsRequired();

            modelBuilder.Entity<Poll>().ToTable("Polls");
            modelBuilder.Entity<Poll>().HasKey(e => e.Id);
            modelBuilder.Entity<Poll>().HasMany(e => e.Votes).WithOne(e => e.Poll).HasForeignKey(e => e.PollId).IsRequired();
            

            modelBuilder.Entity<Vote>().ToTable("Votes");
            modelBuilder.Entity<Vote>().HasKey(e => e.Id);
            modelBuilder.Entity<Vote>().Property(e => e.UpVote).IsRequired();
            modelBuilder.Entity<Vote>().Property(e => e.User).HasMaxLength(100).IsRequired();
        }
    }
}
