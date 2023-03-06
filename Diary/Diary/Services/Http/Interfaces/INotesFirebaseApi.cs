using Diary.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary.Services
{
    public interface INotesFirebaseApi
    {
        [Get("/notes/{id}/{date}.json")]
        public Task<List<Note>> GetNotes([AliasAs("id")] string id, [AliasAs("date")] string date);

        [Put("/notes/{id}/{date}/{noteId}.json")]
        public Task<Note> PutNote([AliasAs("id")] string id, [AliasAs("date")] string date, Note note, [AliasAs("noteId")] string noteId);

        [Put("/notes/{id}/{date}.json")]
        public Task<List<Note>> PutNotes([AliasAs("id")] string id, [AliasAs("date")] string date, List<Note> notes);

        [Patch("/notes/{id}/{date}/{noteId}.json")]
        public Task<Note> EditNote([AliasAs("id")] string id, [AliasAs("date")] string date, Note note, [AliasAs("noteId")] string noteId);

    }
}
