

using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class RegisterUserDTO
    {
        [Required]
        public string? name {get; set;} = string.Empty;

        [Required, EmailAddress]
        public string? email { get; set; } = string.Empty;

        [Required]
        public string? password { get; set; } = string.Empty;

        [Required, Compare(nameof(password))]

        public string? Confirmpassword { get; set; } = string.Empty;
    }
}
