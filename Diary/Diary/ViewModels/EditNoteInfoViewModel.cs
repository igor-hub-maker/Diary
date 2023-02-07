using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    class EditNoteInfoViewModel : BaseViewModel
    {
        public EditNoteInfoViewModel(Note note)
        {
            CancelCommand = new Command(()=>Cancel());
            NextCommand = new Command(()=>Next());
            DelNoteCommand = new Command(()=>DeleteNote());

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
            get => noteTitle;
            set => SetProperty(ref noteTitle, value);
        }
        private string noteDescription;
        public string NoteDescription
        {
            get => noteDescription;
            set => SetProperty(ref noteDescription, value);
        }

        private async Task Cancel()
        {
           await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
        private async Task Next()
        {
            if (string.IsNullOrEmpty(NoteTitle))
            { 
                NavigationDispatcher.Instance.Navigation.ShowPopup(new TitleNotFilledPopup());
                return;
            }
            newNote.Title = noteTitle;
            newNote.Description = noteDescription;
            await NavigationDispatcher.Instance.Navigation.PushAsync(new EditNoteTimePage(newNote, oldNote));
        }
        private async Task DeleteNote()
        {
            bool? answer = (bool)await NavigationDispatcher.Instance.Navigation.ShowPopupAsync(new DeleteConfirmationPopup());
            if (answer is true)
            {
                NoteSerelizer.DeleteNote(oldNote);
                await NavigationDispatcher.Instance.Navigation.PopToRootAsync();
            }
        }
    }
}
