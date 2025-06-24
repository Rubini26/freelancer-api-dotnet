using FreelancerDirectoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FreelancerDirectoryApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Freelancer> Freelancers { get; set; }
        public DbSet<SkillSet> SkillSets { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: configure relationships
            modelBuilder.Entity<SkillSet>()
                .HasOne(s => s.Freelancer)
                .WithMany(f => f.SkillSets)
                .HasForeignKey(s => s.FreelancerId);

            modelBuilder.Entity<Hobby>()
                .HasOne(h => h.Freelancer)
                .WithMany(f => f.Hobbies)
                .HasForeignKey(h => h.FreelancerId);
        }
    }
}
