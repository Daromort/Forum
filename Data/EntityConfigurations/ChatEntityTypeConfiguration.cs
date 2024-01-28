using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class ChatEntityTypeConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).HasColumnName("ChatID").ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();

            builder.HasMany(c => c.ChatsUsers)
                   .WithOne()
                   .HasForeignKey(cu => cu.ChatID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
