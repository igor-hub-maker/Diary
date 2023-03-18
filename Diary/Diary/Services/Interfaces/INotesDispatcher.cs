using Diary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary.Services.Interfaces
{
    public interface INotesDispatcher
    {
        public Task<List<Note>> GetNotes(DateTime date);
        public Task SaveNote(Note note);
        public Task DeleteNote(Note note);
        public Task EditNote(Note note);
        public Task<int> GetNotesCount(DateTime date);
        public Task SaveToLocalNotes();
        public Task UploadFromLocalNotes();
        public bool IsInternetConectionEnable();
    }
}
