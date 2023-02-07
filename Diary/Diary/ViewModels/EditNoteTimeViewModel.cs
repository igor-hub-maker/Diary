using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class EditNoteTimeViewModel : BaseViewModel
    {
        public EditNoteTimeViewModel(Note newNote, Note oldNote)
        {
            SaveNoteCommand = new Command(()=>SaveNote());
            ReturnCommand = new Command(() => Return());

            this.newNote = newNote;
            this.oldNote = oldNote;
            selectedTime = newNote.Date.TimeOfDay;
            selectedDate = newNote.Date;
        }
        private Note newNote;
        private Note oldNote;

        public ICommand SaveNoteCommand { get; }
        public ICommand ReturnCommand { get; }

        private TimeSpan selectedTime;
        public TimeSpan SelectedTime
        {
            get => selectedTime;
            set => SetProperty(ref selectedTime, value);
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        private async Task SaveNote()
        {
            newNote.Date = selectedDate;
            newNote.Time = $"{selectedTime.Hours}:{selectedTime.Minutes}";
            NoteSerelizer.EditNote(newNote, oldNote);
            await NavigationDispatcher.Instance.Navigation.PopToRootAsync();
        }
        private async Task Return()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}
