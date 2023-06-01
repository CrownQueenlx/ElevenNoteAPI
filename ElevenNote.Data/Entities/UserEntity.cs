using System.ComponentModel.DataAnnotations;

namespace ElevenNote.Data.Entities
{
    public class UserEntity
    {
        // Id
        [Key] //automatically considered required
        public int Id { get; set; }

        // Email
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        // Username
        [Required]
        public string? UserName { get; set; }

        // Password
        [Required]
        public string? Password { get; set; }

        // First Name
        public string? NameName { get; set; }

        // Last Name
        public string? LastName { get; set; }

        // Registration (datetime) 
        [Required]
        public DateTime DateCreated { get; set; }
    }
}