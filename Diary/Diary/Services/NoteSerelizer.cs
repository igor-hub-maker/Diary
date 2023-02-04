using Diary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public static void SerelizeNote( Note newNote)
        {
            string fileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + newNote.Date.ToShortDateString() + ".json";
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

        public static void EditNote(Note newNote, Note oldNote)
        {
            string fileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + oldNote.Date.ToShortDateString() + ".json";
            var notes = new List<Note>();
            if (File.Exists(fileName))
            {
                string oldJson = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(oldJson))
                {
                    notes = JsonSerializer.Deserialize<List<Note>>(oldJson);
                }
            }
            var index = notes.IndexOf(notes.Where(n => n.Title == oldNote.Title).FirstOrDefault());
            if (oldNote.Date == newNote.Date)
            {
                notes[index] = newNote;
            }
            else
            {
                notes.RemoveAt(index);
                SerelizeNote(newNote);
            }
            string json = JsonSerializer.Serialize(notes);
            File.WriteAllText(fileName, json);
        }

        public static void DeleteNote(Note NoteToDelete)
        {
            string fileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + NoteToDelete.Date.ToShortDateString() + ".json";
            var notes = new List<Note>();
            if (File.Exists(fileName))
            {
                string oldJson = File.ReadAllText(fileName);
                if (!string.IsNullOrEmpty(oldJson))
                {
                    notes = JsonSerializer.Deserialize<List<Note>>(oldJson);
                }
            }
            var index = notes.IndexOf(notes.Where(n => n.Title == NoteToDelete.Title).FirstOrDefault());
            notes.RemoveAt(index);
            string json = JsonSerializer.Serialize(notes);
            File.WriteAllText(fileName, json);
        }

        public static int GetTodayNotesCount()
        {
            return DeserelizeNotes(DateTime.Now).Count;
        }
    }
}
