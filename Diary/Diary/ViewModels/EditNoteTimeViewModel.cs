using Diary.Models;
using Diary.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class EditNoteTimeViewModel : BaseViewModel
    {
        public EditNoteTimeViewModel(Note newNote)
        {
            SaveNoteCommand = new AsyncCommand(SaveNote);
            ReturnCommand = new AsyncCommand(Return);

            this.newNote = newNote;
            selectedTime = newNote.Date.TimeOfDay;
        }
        private Note newNote;

        public ICommand SaveNoteCommand { get; }
        public ICommand ReturnCommand { get; }

        private TimeSpan selectedTime;
        public TimeSpan SelectedTime
        {
            get => selectedTime;
            set => SetProperty(ref selectedTime, value);
        }
        private async Task SaveNote()
        {
            newNote.Time = DateTime.Today.Add(selectedTime).ToShortTimeString();
            await DependencyService.Get<INotesDispatcher>().EditNote(newNote);
            await NavigationDispatcher.Instance.Navigation.PopToRootAsync();
        }
        private async Task Return()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}
