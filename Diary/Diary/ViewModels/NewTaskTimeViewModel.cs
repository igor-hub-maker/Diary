using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class NewTaskTimeViewModel : BaseViewModel
    {
        public NewTaskTimeViewModel(Note note)
        {

            NewNote = note;
            SelectedDate = NewNote.Date;
            SaveNoteCommand = new Command(SaveTask);
            ReturnCommand = new Command(Return);
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

        public void SaveTask()
        {
            NewNote.Date = SelectedDate.Add(SelectedTime);
            NewNote.Time = $"{selectedTime.Hours}:{selectedTime.Minutes}";
            NoteSerelizer.SerelizeNote(NewNote.Date, NewNote);
            App.Current.MainPage = new PlanerPage();
        }

        public void Return()
        {
            App.Current.MainPage = new NewTaskInfoPage(NewNote.Date);
        }
    }
}
