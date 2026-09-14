using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities.Enrollments;

namespace TrainingCenter.Api.Data.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FinalGrade)
       .HasColumnType("decimal(5,2)");

        builder.HasOne(e => e.Student)
               .WithMany(s => s.Enrollments)
               .HasForeignKey(e => e.StudentId)
               .IsRequired();

        builder.HasOne(e => e.TrainingTrack)
               .WithMany(t => t.Enrollments)
               .HasForeignKey(e => e.TrainingTrackId)
               .IsRequired();
    }
}