using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevenNote.Models.Note
{
    public class NoteDetail
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedUtc { get; set; }
        public DateTime? ModifiedUtc { get; set; }
    }
}