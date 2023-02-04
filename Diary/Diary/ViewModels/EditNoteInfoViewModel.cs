using Diary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Windows.Input;
using Diary.Views;
using Diary.Services;

namespace Diary.ViewModels
{
    class EditNoteInfoViewModel: BaseViewModel
    {
        public EditNoteInfoViewModel(Note note)
        {
            CancelCommand = new Command(Cancel);
            NextCommand =   new Command(Next);
            DelNoteCommand = new Command(DeleteNote);

            oldNote = note;
            newNote = new Note();
            newNote.Time = note.Time;
            newNote.Date = note.Date; 
            NoteTitle = note.Title;
            noteDescription = note.Description;
        }

        private Note newNote;
        private Note oldNote;

        public ICommand CancelCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand DelNoteCommand { get; }

        private string noteTitle;
        public string NoteTitle 
        { 
            get=>noteTitle;
            set => SetProperty(ref noteTitle, value);
        }
        private string noteDescription;
        public string NoteDescription
        {
            get => noteDescription;
            set => SetProperty(ref noteDescription, value);
        }

        private void Cancel()
        {
            App.Current.MainPage = new NoteDescriptionPage(newNote);
        }
        private void Next()
        {
            newNote.Title = noteTitle;
            newNote.Description = noteDescription;
            App.Current.MainPage = new EditNoteTimePage(newNote,oldNote);
        }
        private void DeleteNote()
        {
            NoteSerelizer.DeleteNote(oldNote);
            App.Current.MainPage = new PlanerPage();
        }
    }
}
