using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(m => m.ID);
            builder.Property(m => m.ID).HasColumnName("ID").ValueGeneratedOnAdd();
            builder.Property(m => m.Content).IsRequired();
            builder.Property(m => m.Sent).IsRequired();

            builder.HasOne(m => m.Chat)
                   .WithMany(c => c.Messages)
                   .HasForeignKey(m => m.ChatID)
                   .IsRequired();

            builder.HasOne(m => m.Sender)
                   .WithMany()
                   .HasForeignKey(m => m.UserID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
