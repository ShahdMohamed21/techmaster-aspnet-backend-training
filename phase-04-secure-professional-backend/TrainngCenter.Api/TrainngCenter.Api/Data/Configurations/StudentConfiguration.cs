using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities;

namespace TrainingCenter.Api.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.FullName).IsRequired().HasMaxLength(100);

            builder.Property(s => s.Email)
           .IsRequired()
           .HasMaxLength(150);

            builder.HasIndex(s => s.Email).IsUnique();

            builder.Property(s => s.PhoneNumber).HasMaxLength(11);

            builder.HasMany(s=>s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
