//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HareKrishnaNamaHattaWebApp.Models;

namespace HareKrishnaNamaHattaWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<VolunteerDevotees> VolunteerDevotees { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<VolunteerService> VolunteerServices { get; set; }

        //Future Improvements
        //public DbSet<Volunteer> Volunteers { get; set; }
        //public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add indexes for Donation table
            modelBuilder.Entity<Donation>()
                .HasIndex(d => d.DonationDate);

            modelBuilder.Entity<Donation>()
                .HasIndex(d => d.Amount);

            // Optional: Indexing for performance on ContactMessages (if needed)
            // modelBuilder.Entity<ContactMessage>()
            //     .HasIndex(c => c.Email);
        }
    }
}