using Diary.Models;
using Diary.Services;
using Diary.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    class EditNoteInfoViewModel : BaseViewModel
    {
        public EditNoteInfoViewModel(Note note)
        {
            CancelCommand = new AsyncCommand(Cancel);
            NextCommand = new AsyncCommand(Next);
            DelNoteCommand = new AsyncCommand(DeleteNote);

            Note = note;
            NoteTitle = note.Title;
            NoteDescription = note.Description;
        }

        private Note Note;

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
            Note.Title = noteTitle;
            Note.Description = noteDescription;
            await NavigationDispatcher.Instance.Navigation.PushAsync(new EditNoteTimePage(Note));
        }
        private async Task DeleteNote()
        {
            bool? answer = (bool)await NavigationDispatcher.Instance.Navigation.ShowPopupAsync(new DeleteConfirmationPopup());
            if (answer is true)
            {
                await DependencyService.Get<INotesDispatcher>().DeleteNote(Note);
                await NavigationDispatcher.Instance.Navigation.PopToRootAsync();
            }
        }
    }
}
