using Microsoft.Extensions.Configuration.UserSecrets;

namespace Forum_Management_System.Models
{
    public class CommentQueryParameters : QueryParameters
    {
        int UserID { get; set; }
    }
}
