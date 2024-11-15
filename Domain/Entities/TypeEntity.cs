
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TypeEntity
    {
        [Key]

        public int id_type { get; set; }

        [Required]

        public string type { get; set; } = string.Empty; 
        
    }
}
