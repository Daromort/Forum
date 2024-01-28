using Forum_Management_System.Models.Enums;

namespace Forum_Management_System.Models
{
    public class QueryParameters : Pagination
    {
        public int? ID { get; set; }
        public string Search { get; set; }
        public SortBy Sort { get; set; }
    }
}
