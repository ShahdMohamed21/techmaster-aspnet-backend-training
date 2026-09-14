using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrainingCenter.Api.Entities.Payments;

namespace TrainingCenter.Api.Data.Configurations
{
    public class PaymentSummaryConfiguration : IEntityTypeConfiguration<PaymentSummary>
    {
        public void Configure(EntityTypeBuilder<PaymentSummary> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.TotalRequired)
              .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(p => p.TotalPaid)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();
            builder.Property(p => p.PaymentStatus)
       .HasConversion<string>()
       .HasMaxLength(20)
       .IsRequired();

            builder.HasOne(p => p.Enrollment)
           .WithOne(e => e.PaymentSummary)
           .HasForeignKey<PaymentSummary>(p => p.EnrollmentId)
           .IsRequired();

            builder.HasIndex(p => p.EnrollmentId)
                   .IsUnique();

        }
    }
}
