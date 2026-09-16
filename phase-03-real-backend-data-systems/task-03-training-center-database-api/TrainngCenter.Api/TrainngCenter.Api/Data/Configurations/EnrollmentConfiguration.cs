using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities;

namespace TrainingCenter.Api.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.EnrollmentId);

            builder.Property(e => e.Status).IsRequired().HasMaxLength(50);

            builder.Property(e => e.ProgressPercentage).HasPrecision(5, 2);

            builder.Property(e => e.FinalResult).HasMaxLength(100);

          
            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

       
            builder.HasOne(e => e.TrainingTrack)
                .WithMany(t => t.Enrollments)
                .HasForeignKey(e => e.TrainingTrackId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Payments)
                .WithOne(p => p.Enrollment)
                .HasForeignKey(p => p.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}