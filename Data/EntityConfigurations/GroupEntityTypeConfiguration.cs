using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class GroupEntityTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            //builder.HasMany(group => group.Users)
            //      .WithMany(users => users.Groups)
            //      .UsingEntity<GroupUser>();

            builder.HasMany(group => group.Posts)
                .WithOne(post => post.Group)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(group => group.Users)
                .WithMany(users => users.Groups)
                .UsingEntity<GroupUser>();

            builder.HasOne(group => group.Creator)
                .WithMany(creator => creator.CreatedGroups)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
