using Diary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Diary.Services
{
    public static class NoteSerelizer
    {
        public static List<Note> DeserelizeNotes(DateTime date)
        {
            var res = new List<Note>();
            string fileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + date.ToShortDateString() + ".json";
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(json))
                {
                    res = JsonSerializer.Deserialize<List<Note>>(json);
                }
            }
            return res;
        }

        public static void SerelizeNote(DateTime date, Note newNote)
        {
            string fileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + date.ToShortDateString() + ".json";
            var notes = new List<Note>();
            if (File.Exists(fileName))
            {
                string oldJson = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(oldJson))
                {
                    notes = JsonSerializer.Deserialize<List<Note>>(oldJson);
                }
            }
            notes.Add(newNote);
            string json = JsonSerializer.Serialize(notes);
            File.WriteAllText(fileName, json);
        }

        public static int GetTodayNotesCount()
        {
            return DeserelizeNotes(DateTime.Now).Count;
        }
    }
}
