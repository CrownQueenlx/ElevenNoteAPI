using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.User
{
    public class UserRegister
    {
        // Email
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Username
        [Required]
        [MinLength(4)]
        public string UserName { get; set; } = string.Empty;

        // Password
        [Required]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare(nameof(Password))]
        // Confirm Password
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}


// DateCreated
// Id


// FirstName Opt
// LastName Opt