using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FolderEntity
    {
        [Key] 

        public int id_folder { get; set; }

        [Required]
        public int id_user_id { get; set; }

        public bool is_public {  get; set; }

        public DateTime created_at { get; set; } 

        [Required]
        [MaxLength(50)]
        public required string name { get; set; } = string.Empty;

        public ICollection<NoteEntity> Notes { get; set; } = new List<NoteEntity>();

        [ForeignKey("id_user_id")]
        public UserEntity User { get; set; } = null!;

    }
}
