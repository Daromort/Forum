namespace Forum_Management_System.Models.DTO
{
    public class GetCommentDTO
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public GetUserDTO User { get; set; }
        public List<GetCommentDTO> Replies { get; set; }
    }
}
