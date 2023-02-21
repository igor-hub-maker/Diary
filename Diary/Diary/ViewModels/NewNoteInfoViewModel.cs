using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Diary.ViewModels
{
    public class NewNoteInfoViewModel : BaseViewModel
    {
        private DateTime taskDate;

        public NewNoteInfoViewModel(DateTime date)
        {
            NewTaskContinueCommand = new AsyncCommand(NewNoteContinue);
            CancelCommand = new AsyncCommand(Cancel);
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

        public async Task NewNoteContinue()
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
