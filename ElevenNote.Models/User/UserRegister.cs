using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Models.User
{
    public class UserRegister
    {
        // Email
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Username
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        // Password
        [Required]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        // Confirm Password
        public string ConfirmPassword { get; set; }

    }
}


// DateCreated
// Id


// FirstName Opt
// LastName Opt