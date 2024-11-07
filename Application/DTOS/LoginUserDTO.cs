
using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class LoginUserDTO
    {
        [Required, EmailAddress]
        public string? email { get; set; } = string.Empty;

        [Required]
        public string? password { get; set; } = string.Empty;
    }
}
