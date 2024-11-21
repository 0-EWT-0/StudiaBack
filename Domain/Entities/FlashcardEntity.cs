using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FlashcardEntity
    {
        [Key]
        public int id_flashcard {  get; set; }

        [Required]
        public int id_user_id { get; set; }

        public int id_type_id { get; set; }

        public DateTime created_at { get; set; }

        [Required]
        public string content { get; set; } = string.Empty;

        public string image_url { get; set; } = string.Empty;

        public bool is_public { get; set; } = false;

        [Required]
        public string name { get; set; } = string.Empty;

        [ForeignKey("id_user_id")]
        public UserEntity User { get; set; } = null!;

        [ForeignKey("id_type_id")]

        public TypeEntity Type { get; set; } = null!;
    }
}
