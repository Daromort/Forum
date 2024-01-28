using Forum_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(c => c.Parent)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentID);

            builder.HasMany(comment => comment.Likes)
                   .WithOne(like => like.Comment)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(comment => comment.Dislikes)
                  .WithOne(dislike => dislike.Comment)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
