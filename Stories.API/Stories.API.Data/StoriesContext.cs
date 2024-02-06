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
            modelBuilder.Entity<Story>().HasKey(s => s.Id);
            modelBuilder.Entity<Story>().Property(s => s.Title).HasMaxLength(80).IsRequired();
            modelBuilder.Entity<Story>().Property(s => s.Description).HasMaxLength(250).IsRequired();


        }
    }
}
