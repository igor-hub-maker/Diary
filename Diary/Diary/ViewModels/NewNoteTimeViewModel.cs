using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class NewNoteTimeViewModel : BaseViewModel
    {
        public NewNoteTimeViewModel(Note note)
        {
            SaveNoteCommand = new Command(()=>SaveNote());
            ReturnCommand = new Command(()=>Return());

            NewNote = note;
            SelectedTime = DateTime.Now.TimeOfDay;
            SelectedDate = NewNote.Date;
        }

        private Note NewNote;

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

        public async Task SaveNote()
        {
            NewNote.Date = SelectedDate.Add(SelectedTime);
            NewNote.Time = $"{selectedTime.Hours}:{selectedTime.Minutes}";
            NoteSerelizer.SerelizeNote(NewNote);
            await NavigationDispatcher.Instance.Navigation.PopToRootAsync();
        }

        public async Task Return()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}
