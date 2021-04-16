using A1.Models;
using Microsoft.EntityFrameworkCore;

namespace A1.Data
{
    public class SchoolCommunityContext : DbContext
    {
        public DbSet<CommunityMembership> CommunityMemberships { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

        public SchoolCommunityContext(DbContextOptions<SchoolCommunityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommunityMembership>()
                .HasKey(c => new { c.StudentID, c.CommunityID });

            modelBuilder.Entity<CommunityMembership>()
                .HasOne(m => m.Student)
                .WithMany(s => s.Membership)
                .HasForeignKey(m => m.StudentID);

            modelBuilder.Entity<CommunityMembership>()
                   .HasOne(m => m.Community)
                   .WithMany(c => c.Membership)
                   .HasForeignKey(m => m.CommunityID);

            modelBuilder.Entity<Community>()
                .HasMany(c => c.Advertisements)
                .WithOne(adv => adv.Community);

            base.OnModelCreating(modelBuilder);
        }
    }
}
