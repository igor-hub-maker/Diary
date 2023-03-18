using Diary.Models;
using Diary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Diary.Services
{
    public class NotesDispatcher : INotesDispatcher
    {
        public NotesDispatcher()
        {
            HasInternetCOnnection = IsInternetConectionEnable();
            LocalNotesService = new LocalNotesService();
            OnlineNoteService = new OnlineNoteService();
        }
        public bool HasInternetCOnnection = false;
        private LocalNotesService LocalNotesService;
        private OnlineNoteService OnlineNoteService;

        public async Task DeleteNote(Note note)
        {
            if (HasInternetCOnnection)
            {
                await OnlineNoteService.DeleteNote(note);
            }
            await LocalNotesService.DeleteNote(note);
        }

        public async Task EditNote(Note note)
        {
            if (HasInternetCOnnection)
            {
                await OnlineNoteService.EditNote(note);
            }
            await LocalNotesService.EditNote(note);
        }

        public async Task<List<Note>> GetNotes(DateTime date)
        {
            var result = new List<Note>();
            if (HasInternetCOnnection)
            {
                result = await OnlineNoteService.GetNotes(date);
            }
            else
            {
                result = await LocalNotesService.GetNotes(date);
            }
            return result;
        }

        public async Task<int> GetNotesCount(DateTime date)
        {
            var result = 0;
            if (HasInternetCOnnection)
            {
                result = await OnlineNoteService.GetNotesCount(date);
            }
            else
            {
                result = await LocalNotesService.GetNotesCount(date);
            }
            return result;
        }

        public async Task SaveNote(Note note)
        {
            if (HasInternetCOnnection)
            {
                await OnlineNoteService.SaveNote(note);
            }
            await LocalNotesService.SaveNote(note);
        }

        public async Task SaveToLocalNotes()
        {
            var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            do
            {
                var notes = await OnlineNoteService.GetNotes(date);
                var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + date.ToShortDateString().Replace('.', '-') + ".json";
                var json = JsonSerializer.Serialize(notes);
                File.WriteAllText(filePath, json);
                date.AddDays(1);
            }
            while (date.Day != lastDay);
        }
        public async Task UploadFromLocalNotes()
        {
            var date = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            do
            {
                var localNotes = await LocalNotesService.GetNotes(date);
                if (!(localNotes is null))
                {
                    await OnlineNoteService.SaveNotes(date, localNotes);
                }
                date.AddDays(1);
            }
            while (date.Day != lastDay);
        }

        public bool IsInternetConectionEnable()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
