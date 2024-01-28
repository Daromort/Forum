using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class ChatUserEntityTypeConfiguration : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.ToTable("ChatUsers");
            builder.HasKey(cu => new { cu.ChatID, cu.UserID });

            builder.HasOne(cu => cu.Chat)
                   .WithMany(c => c.ChatsUsers)
                   .HasForeignKey(cu => cu.ChatID)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cu => cu.User)
                   .WithMany(u => u.ChatsUsers)
                   .HasForeignKey(cu => cu.UserID)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
