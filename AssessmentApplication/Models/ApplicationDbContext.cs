using AssessmentApplication.Seeders;
using Microsoft.EntityFrameworkCore;

namespace AssessmentApplication.Models
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        internal DbSet<FeebackModel> FeebackModels { get; set; }
        internal DbSet<AttachmentModel> AttachmentModels { get; set; }
        internal DbSet<ServiceModel> ServiceModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship
            modelBuilder.Entity<FeedbackService>()
                .HasKey(fs => new { fs.FeedbackModelId, fs.ServiceModelId });

            modelBuilder.Entity<FeedbackService>()
                .HasOne(fs => fs.FeedbackModel)
                .WithMany(f => f.FeedbackServices)
                .HasForeignKey(fs => fs.FeedbackModelId);

            modelBuilder.Entity<FeedbackService>()
                .HasOne(fs => fs.ServiceModel)
                .WithMany(s => s.FeedbackServices)
                .HasForeignKey(fs => fs.ServiceModelId);

            // Rename tables
            modelBuilder.Entity<FeebackModel>().ToTable("FeedBack");
            modelBuilder.Entity<AttachmentModel>().ToTable("Attachments");
            modelBuilder.Entity<ServiceModel>().ToTable("Services");

            ServiceSeeder.SeedServices(modelBuilder); // Call the seeder
        }

        // Define DbSets and other context-related methods or properties here
    }

    
}
