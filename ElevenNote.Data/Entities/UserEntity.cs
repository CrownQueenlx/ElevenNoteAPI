namespace ElevenNote.Data.Entities
{
    public class UserEntity
    {
        // Id
        public int Id { get; set; }
        // Email
        public string? Email { get; set; }
        // Username
        public string? UserName { get; set; }
        // Password
        public string? Password { get; set; }
        // First Name
        public string? NameName { get; set; }
        // Last Name
        public string? LastName { get; set; }
        // Registration (datetime) 
        public DateTime DateCreated { get; set; }
    }
}