using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class PostDislikeEntityTypeConfiguration : IEntityTypeConfiguration<PostDislike>
    {
        public void Configure(EntityTypeBuilder<PostDislike> builder)
        {
            builder.HasKey(d => new { d.PostID, d.UserID });
        }
    }
}
