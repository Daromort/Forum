using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class CommentDislikeEntityTypeConfiguration : IEntityTypeConfiguration<CommentDislike>
    {
        public void Configure(EntityTypeBuilder<CommentDislike> builder)
        {
            builder.HasKey(x => new { x.UserID, x.CommentID });
        }
    }
}
