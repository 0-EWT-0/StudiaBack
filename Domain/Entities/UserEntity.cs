
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEntity
    {
        [Key]
        public int id_user { get; set; }
        public int user_type { get; set; }

        [Required]
        public required string name { get; set; }

        [Required]
        public required string password { get; set; }

        [Required]
        public required string email { get; set; }
        public DateTime created_at { get; set; }

    }
}
