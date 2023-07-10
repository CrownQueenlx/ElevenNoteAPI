using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ElevenNote.Data;
using ElevenNote.Models.Note;
using ElevenNote.Data.Entities;

namespace ElevenNote.Services.Note;

public class NoteService : INoteService
{
    private readonly int _userId;
    private readonly ApplicationDbContext _dbContext;
    public NoteService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
    {
        var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        var value = userClaims?.FindFirst("Id")?.Value;
        var validId = int.TryParse(value, out _userId); //this is setting as a field
        if (!validId)
            throw new Exception("Attempted to build NoteService without User Id claim.");

        _dbContext = dbContext;

    }

    public async Task<NoteListItem?> CreateNoteAsync(NoteCreate request)
    {
        var noteEntity = new NoteEntity
        {

            Title = request.Title,
            Content = request.Content,
            CreatedUtc = DateTime.Now,
            OwnerId = _userId
        };

        _dbContext.Notes.Add(noteEntity);
        var numberOfChanges = await _dbContext.SaveChangesAsync();

        if (numberOfChanges == 1)
        {
            NoteListItem response = new()
            {
                Id = noteEntity.Id,
                Title = noteEntity.Title,
                CreatedUtc = noteEntity.CreatedUtc,
            };
            return response;
        }

        return null;
    }

    public async Task<IEnumerable<NoteListItem>> GetAllNotesAsync()
    {
        var notes = await _dbContext.Notes
        .Where(entity => entity.OwnerId == _userId)
        .Select(entity => new NoteListItem
        {
            Id = entity.Id,
            Title = entity.Title,
            CreatedUtc = entity.CreatedUtc
        })
        .ToListAsync();

        return notes;
    }
    public async Task<NoteDetail?> GetNoteByIdAsync(int noteId)
    {
        // Find the first note that has the given Id
        // and an OwnerId htat matches the requesting _userId
        var noteEntity = await _dbContext.Notes
        .FirstOrDefaultAsync(e =>
        e.Id == noteId && e.OwnerId == _userId
        );
        // If noteEntity is null then return null
        // Otherwise initialize and reutrn a new NoteDetail
        return noteEntity is null ? null : new NoteDetail
        {
            Id = noteEntity.Id,
            Title = noteEntity.Title,
            Content = noteEntity.Content,
            CreatedUtc = noteEntity.CreatedUtc,
            ModifiedUtc = noteEntity.ModifiedUtc
        };
    }
    public async Task<bool> UpdateNoteAsync(NoteUpdate request)
    {
        // Find the note and validate it's owned by the user
        var noteEntity = await _dbContext.Notes.FindAsync(request.Id);

        // by using the null conditional operator we can check if it's null 
        //  and at the same time we check the OwnerId vs the _userId
        if (noteEntity?.OwnerId != _userId)
            return false;

        // Now we update the entity's properties
        noteEntity.Title = request.Title;
        noteEntity.Content = request.Content;
        noteEntity.ModifiedUtc = DateTimeOffset.Now;

        // Save the changes to tthe database and capture how many rows were updated
        var numberOfChanges = await _dbContext.SaveChangesAsync();

        // numberOfChanges is stated to be equal to 1 because only one row is updated
        return numberOfChanges == 1;
    }
    public async Task<bool> DeleteNoteAsync(int noteId)
    {
        // Find the note by the given Id
        var noteEntity = await _dbContext.Notes.FindAsync(noteId);

        // Validate the note exists and is owned by the user
        if (noteEntity?.OwnerId != _userId)
            return false;

        // Remove the note from the DbContext and assert that the one change was saved
        _dbContext.Notes.Remove(noteEntity);
        return await _dbContext.SaveChangesAsync() == 1;
    }


}