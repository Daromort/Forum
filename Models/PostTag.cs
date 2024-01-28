namespace Forum_Management_System.Models
{
    //ToDo
    // ID primary key to be replaced with Name
    public class PostTag
    {
        public int PostID { get; set; }
        public Post Post { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }

    }
}
