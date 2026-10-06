using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities;

namespace TrainingCenter.Api.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.Amount).IsRequired().HasPrecision(18, 2);

            builder.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);

            builder.Property(p => p.PaymentStatus).IsRequired().HasMaxLength(50);

            builder.Property(p => p.ReferenceNumber).IsRequired().HasMaxLength(100);

            builder.Property(p => p.Notes).HasMaxLength(500);


            builder.HasOne(p => p.Enrollment)
                .WithMany(e => e.Payments)
                .HasForeignKey(p => p.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}