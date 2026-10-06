using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities;

namespace TrainingCenter.Api.Data.Configurations
{
    public class TrainingTrackConfiguration : IEntityTypeConfiguration<TrainingTrack>
    {
        public void Configure(EntityTypeBuilder<TrainingTrack> builder)
        {
            
            builder.HasKey(t => t.TrainingTrackId);

           
            builder.Property(t => t.Title).IsRequired() .HasMaxLength(150);

            builder.Property(t => t.Code).IsRequired().HasMaxLength(50);

           
            builder.HasIndex(t => t.Code).IsUnique();

            builder.Property(t => t.Description).HasMaxLength(500);

            builder.Property(t => t.Level).IsRequired().HasMaxLength(50);

            builder.Property(t => t.Status).IsRequired().HasMaxLength(50);

            
            builder.HasOne(t => t.Instructor)
                .WithMany(i => i.TrainingTracks)
                .HasForeignKey(t => t.InstructorId)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasMany(t => t.Enrollments)
                .WithOne(e => e.TrainingTrack)
                .HasForeignKey(e => e.TrainingTrackId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}