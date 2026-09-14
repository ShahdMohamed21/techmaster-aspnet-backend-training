using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities.Tracks;

namespace TrainingCenter.Api.Data.Configurations;

public class TrainingTrackConfiguration: IEntityTypeConfiguration<TrainingTrack>
{
    public void Configure(EntityTypeBuilder<TrainingTrack> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.HasOne(t => t.Instructor)
               .WithMany(i => i.TrainingTracks)
               .HasForeignKey(t => t.InstructorId)
               .IsRequired();
    }
}