
using System.Security;

namespace Application.DTOS.Responses
{
    public class RatingResponse
    {
        public string Response { get; set; } = string.Empty;
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
