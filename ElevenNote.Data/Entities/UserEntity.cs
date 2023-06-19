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
        public string Email { get; set; } = string.Empty;

        // Username
        [Required]
        public string UserName { get; set; } = string.Empty;

        // Password
        [Required]
        public string Password { get; set; } = string.Empty;

        // First Name
        public string? FirstName { get; set; } 

        // Last Name
        public string? LastName { get; set; }

        // Registration (datetime) 
        [Required]
        public DateTime DateCreated { get; set; }
        public List<NoteEntity> Notes { get; set; } = new();
    }
    
}