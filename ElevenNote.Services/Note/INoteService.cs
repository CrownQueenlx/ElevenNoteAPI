using ElevenNote.Models.Note;

namespace ElevenNote.Services.Note;

public interface INoteService
{
    Task<NoteListItem?> CreateNoteAsync(NoteCreate request); //(request model type and parameter)
    Task<IEnumerable<NoteListItem>> GetAllNotesAsync();
}