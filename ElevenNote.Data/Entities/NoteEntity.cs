

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElevenNote.Data.Entities
{
    public class NoteEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Owner))]
        public int OwnerId { get; set; }
         public UserEntity Owner { get; set; } = null!;
        [Required]
        [MinLength(2), MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MaxLength (8000)]
        public string Content { get; set; } = string.Empty;
        // string.empty gives defalt value, will never be null
        [Required]
        public DateTime CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; } //Coordinated Universal Time
       
    }
}