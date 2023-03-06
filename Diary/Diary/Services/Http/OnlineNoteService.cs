using Diary.Models;
using Diary.Services.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Diary.Services
{
    public class OnlineNoteService
    {
        private INotesFirebaseApi restService = RestService.For<INotesFirebaseApi>("https://diary-c636d-default-rtdb.europe-west1.firebasedatabase.app");
        public async Task DeleteNote(Note note)
        {
            var notes = await GetNotes(note.Date);
            notes.RemoveAt(note.Id);
            for (int i = 0;i<notes.Count;i++)
            {
                notes[i].Id = i;
            }
            var datePath = note.Date.ToShortDateString().Replace('.', '-');
            await restService.PutNotes(LocalUserInfoService.Id.ToString(), datePath, notes);
        }

        public async Task EditNote(Note note)
        {
            var datePath = note.Date.ToShortDateString().Replace('.', '-');
            await restService.EditNote(LocalUserInfoService.Id.ToString(), datePath, note, note.Id.ToString());
        }

        public async Task<List<Note>> GetNotes(DateTime date)
        {
            var datestr = date.ToShortDateString().Replace('.', '-');
            var result = new List<Note>();
            result = await restService.GetNotes(LocalUserInfoService.Id.ToString(), datestr);
            if (result is null)
            {
                result = new List<Note>();
            }
            return result;
        }

        public async Task<int> GetNotesCount(DateTime date)
        {
            int res = (await GetNotes(date)).Count;
            return res;
        }

        public async Task SaveNote(Note note)
        {
            int id = await GetNotesCount(note.Date);
            note.Id = id;

            var datePath = note.Date.ToShortDateString().Replace('.', '-');
            await restService.PutNote(LocalUserInfoService.Id.ToString(), datePath, note, note.Id.ToString());
        }
        public async Task SaveNotes(DateTime date,List<Note> notes)
        {
            var datePath = date.ToShortDateString().Replace('.', '-');
            await restService.PutNotes(LocalUserInfoService.Id.ToString(),datePath,notes);
        }
    }
}
