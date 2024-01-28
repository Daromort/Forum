using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasIndex(u => u.Email)
                    .IsUnique();

            builder.HasMany(user => user.Posts)
                   .WithOne(post => post.User)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.Comments)
                   .WithOne(comment => comment.User)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(user => user.PostLikes)
                    .WithOne(like => like.User)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.PostDislikes)
                   .WithOne(dislike => dislike.User)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.CommentLikes)
                    .WithOne(commentLike => commentLike.User)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.CommentDislikes)
                   .WithOne(commentDislikes => commentDislikes.User)
                   .OnDelete(DeleteBehavior.Cascade);

           

        }
    }
}
