using LaikableDogsAPI.Models.Enums;

namespace LaikableDogsAPI.Models.Requests
{
    public class SortingRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public SortingDirection SortingDirection { get; set; }
    }
}
