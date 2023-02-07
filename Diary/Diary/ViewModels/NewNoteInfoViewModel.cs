using Diary.Models;
using Diary.Views;
using Diary.Services;
using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Diary.ViewModels
{
    public class NewNoteInfoViewModel : BaseViewModel
    {
        private DateTime taskDate;

        public NewNoteInfoViewModel(DateTime date)
        {
            NewTaskContinueCommand = new Command(()=>NewNoteContinue());
            CancelCommand = new Command(()=>Cancel());
            taskDate = date;
        }

        public ICommand CancelCommand { get; }
        public ICommand NewTaskContinueCommand { get; }

        private string noteTitle;
        public string NoteTitle
        {
            get => noteTitle;
            set
            {
                noteTitle = value;
                OnPropertyChanged();
            }
        }

        private string noteDescription;
        public string NoteDescription
        {
            get => noteDescription;
            set
            {
                noteDescription = value;
                OnPropertyChanged();
            }
        }

        public async Task Cancel()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }

        public async void NewNoteContinue()
        {
            if (string.IsNullOrEmpty(noteTitle))
            {
                 NavigationDispatcher.Instance.Navigation.ShowPopup(new TitleNotFilledPopup());
            }
            else
            {
                Note note = new Note();
                note.Date = taskDate;
                note.Title = NoteTitle;
                note.Description = NoteDescription;
                await NavigationDispatcher.Instance.Navigation.PushAsync(new NewNoteTimePage(note));
            }
        }
    }
}
