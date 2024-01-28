namespace Forum_Management_System.Models
{
    public class UserQueryParameters : QueryParameters
    {
        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
        public UserQueryParameters Clone(int pageIncrement)
        {
            return new UserQueryParameters
            {
                ID = this.ID,
                FirstName = this.FirstName,
                Email = this.Email,
                Username = this.Username,
                Page = this.Page + pageIncrement,
                Size = this.Size,
                Search = this.Search
            };
        }
    }
}
