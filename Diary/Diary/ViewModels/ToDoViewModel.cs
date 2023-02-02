using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class ToDoViewModel : BaseViewModel
    {
        public ToDoViewModel(DateTime date)
        {
            NewNoteCommand = new Command(NewNote);
            OpenPlanerPageCommand = new Command(OpenPlanerPage);
            PageDate = date;
            PageTitle = $"{PageDate.Day} // {PageDate.Month}";
            WeekDay = PageDate.DayOfWeek.ToString();
            Notes = NoteSerelizer.DeserelizeNotes(PageDate);
        }

        private DateTime PageDate;

        public ICommand OpenPlanerPageCommand { get; }
        public ICommand NewNoteCommand { get; }

        private List<Note> notes;
        public List<Note> Notes
        {
            get => notes;
            set => SetProperty(ref notes, value);
        }

        private string pageTitle;
        public string PageTitle
        {
            get => pageTitle;
            set
            {
                pageTitle = value;
                OnPropertyChanged();
            }
        }
        private string weekDay;
        public string WeekDay
        {
            get => weekDay;
            set
            {
                weekDay = value;
                OnPropertyChanged();
            }
        }

        public void OpenPlanerPage()
        {
            App.Current.MainPage = new PlanerPage();
        }

        public void NewNote()
        {
            App.Current.MainPage = new NewTaskInfoPage(PageDate);
        }
    }
}
