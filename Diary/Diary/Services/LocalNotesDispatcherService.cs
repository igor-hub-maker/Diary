using Diary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diary.Services
{
    public class LocalNotesDispatcherService : INotesDispatcher
    {
        public async Task DeleteNote(Note note)
        {
            var notes = await GetNotes(note.Date);
            notes.RemoveAt(note.Id);
            SerializeNotes(note.Date, notes);
        }

        public async Task EditNote(Note note)
        {
            var notes = await GetNotes(note.Date);
            notes[note.Id] = note;
            SerializeNotes(note.Date, notes);
        }

        public Task<List<Note>> GetNotes(DateTime date)
        {
            var res = new List<Note>();
            string filePath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + date.ToShortDateString().Replace('.', '-') + ".json";
            if(!File.Exists(filePath))
            {
                return Task.FromResult(res);
            }
            var json = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(json))
            {
               res = JsonSerializer.Deserialize<List<Note>>(json);
            }
            return Task.FromResult(res);
        }

        public async Task<int> GetNotesCount(DateTime date)
        {
            int res = (await GetNotes(date)).Count;
            return res;
        }

        public async Task SaveNote(Note note)
        {
            var notes = await GetNotes(note.Date);
            notes.Add(note);
            SerializeNotes(note.Date,notes);
        }
        
        private void SerializeNotes(DateTime date, List<Note> notes)
        {
            string filePath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + date.ToShortDateString().Replace('.', '-') + ".json";
            var json = JsonSerializer.Serialize(notes);
            File.WriteAllText(filePath, json);
        }
    }
}
