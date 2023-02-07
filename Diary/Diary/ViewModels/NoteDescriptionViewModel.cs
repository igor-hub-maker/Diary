using Diary.Models;
using Diary.Services;
using Diary.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class NoteDescriptionViewModel : BaseViewModel
    {
        public NoteDescriptionViewModel(Note note)
        {
            EditNoteCommand = new Command(()=>EditNote());
            GoBackCommand = new Command(()=>GoBack());

            NoteTitle = note.Title;
            Time = note.Time;
            Date = $"{note.Date.Day} // {note.Date.Month}";
            Description = note.Description;
            this.note = note;
        }

        private Note note;

        public ICommand EditNoteCommand { get; }
        public ICommand GoBackCommand { get; }

        private string noteTitle;
        public string NoteTitle
        {
            get => noteTitle;
            set => SetProperty(ref noteTitle, value);
        }
        private string time;
        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
        private string date;
        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private async Task EditNote()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new EditNoteInfoPage(note));
        }
        private async Task GoBack()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}
