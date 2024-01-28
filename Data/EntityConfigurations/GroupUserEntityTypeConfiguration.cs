using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum_Management_System.Data.EntityConfigurations
{
    public class GroupUserEntityTypeConfiguration : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.HasKey(l => new { l.GroupID, l.UserID });
        }
    }
}
