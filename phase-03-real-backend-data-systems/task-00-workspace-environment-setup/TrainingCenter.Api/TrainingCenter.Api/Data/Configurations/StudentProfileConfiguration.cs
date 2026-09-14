using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities.Students;

namespace TrainingCenter.Api.Data.Configurations
{
    public class StudentProfileConfiguration : IEntityTypeConfiguration<StudentProfile>
    {
        public void Configure(EntityTypeBuilder<StudentProfile> builder) {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Student)
               .WithOne(s => s.Profile)
               .HasForeignKey<StudentProfile>(p => p.StudentId)
               .IsRequired();


        }
    }
}
