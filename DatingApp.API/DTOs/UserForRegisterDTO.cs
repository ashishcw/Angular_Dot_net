using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.DTOs
{
    public class UserForRegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 4, ErrorMessage="Password must be greater than 4 and lower than 12 characterts in length")]
        public string Password { get; set; }
    }
}