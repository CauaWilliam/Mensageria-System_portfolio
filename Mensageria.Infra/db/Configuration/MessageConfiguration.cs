using Mensageria.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mensageria.Infra.db.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.ToTable("messages");
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id")
            .HasMaxLength(50);
        
        builder.HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(m => m.Content)
            .HasColumnName("content")
            .HasMaxLength(4000)
            .IsRequired();
        
        builder.HasMany(m => m.Recipients)
            .WithOne()
            .HasForeignKey(m => m.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(m => m.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(u => u.SentAt)
            .HasColumnName("sent_at")
            .HasColumnType("timestamp with time zone")
            .HasConversion(
                isoString => isoString != null ? DateTimeOffset.Parse(isoString) : (DateTimeOffset?)null,
                dateTimeOffset => dateTimeOffset.HasValue ? dateTimeOffset.Value.ToString("o") : null);
        
        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .HasConversion(
                isoString => string.IsNullOrEmpty(isoString) ? DateTimeOffset.UtcNow : DateTimeOffset.Parse(isoString),
                dateTimeOffset => dateTimeOffset.ToString("o"))
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .HasConversion(
                isoString => isoString != null ? DateTimeOffset.Parse(isoString) : (DateTimeOffset?)null,
                dateTimeOffset => dateTimeOffset.HasValue ? dateTimeOffset.Value.ToString("o") : null);
    }
}