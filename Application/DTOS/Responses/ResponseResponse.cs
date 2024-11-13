
namespace Application.DTOS.Responses
{
    public class ResponseResponse
    {
        public string result { get; set; } = string.Empty;

        public int responseId { get; set; }

        public int id_user_id { get; set; }

        public string response { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

    }
}
