
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ResponseEntity
    {
        [Key]
        public int id_response {  get; set; }

        [Required]
        public int id_user_id { get; set; }

        [Required]
        public string response { get; set; } = string.Empty;

        public DateTime created_at { get; set; }

        [ForeignKey("id_user_id")]

        public UserEntity User { get; set; } = null!;
    }
}
