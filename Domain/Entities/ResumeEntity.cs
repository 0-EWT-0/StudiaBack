
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ResumeEntity
    {
        [Key]
        public int id_resume { get; set; }

        public int id_user_id { get; set; }

        public int? id_type_id { get; set; }

        public DateTime created_at { get; set; }

        [Required]
        public string content { get; set; } = string.Empty;

        public string image_url { get; set; } = string.Empty;

        public bool is_public { get; set; }

        [ForeignKey("id_user_id")]
        public UserEntity User { get; set; } = null!;

        //[ForeignKey("id_type_id")]

        //public TypeEntity Type { get; set; }
    }
}
