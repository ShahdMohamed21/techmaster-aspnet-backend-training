using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities;

namespace TrainingCenter.Api.Data.Configurations
{
    public class InstructorConfiguration:IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.HasKey(I => I.InstructorId);

            builder.Property(I => I.FullName).IsRequired().HasMaxLength(100);

            builder.Property(I => I.Email).IsRequired().HasMaxLength(150);

            builder.HasIndex(I => I.Email).IsUnique();

            builder.HasMany(I=>I.TrainingTracks)
            .WithOne(T=>T.Instructor)
            .HasForeignKey(T=>T.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
