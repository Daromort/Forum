using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(post => post.Tags)
                    .WithMany(tags => tags.Posts)
                    .UsingEntity<PostTag>();

            builder.HasMany(post => post.Comments)
                    .WithOne(comment => comment.Post)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(post => post.Likes)
                   .WithOne(like => like.Post)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(post => post.Dislikes)
                  .WithOne(dislike => dislike.Post)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
