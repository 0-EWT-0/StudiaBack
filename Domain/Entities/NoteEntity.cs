using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class NoteEntity
    {
        [Key]

        public int id_note { get; set; }

        [Required]
        public int id_folder_id {  get; set; }

        public bool is_public { get; set; } = false;

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public required string content { get; set; }

        [ForeignKey("id_folder_id")]

        public FolderEntity Folder {  get; set; }

    }
}
