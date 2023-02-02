using Diary.Services;
using Diary.Views;
using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class PlanerViewModel : BaseViewModel
    {
        public PlanerViewModel()
        {
            Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("en-GB");
            Mounth = DateTime.Now.Month;
            TodayNotesCount = NoteSerelizer.GetTodayNotesCount();
            SelectedDateCommand = new Command(DateSelected);
            OpenToDoPageCommand = new Command(OpenToDoPage);
        }

        public ICommand SelectedDateCommand { get; }
        public ICommand OpenToDoPageCommand { get; }

        private int todayNotesCount;
        public int TodayNotesCount
        {
            get => todayNotesCount;
            set => SetProperty(ref todayNotesCount, value);
        }

        private CultureInfo culture;
        public CultureInfo Culture
        {
            get => culture;
            set => SetProperty(ref culture, value);
        }

        private int mounth;
        public int Mounth
        {
            get => mounth;
            set => SetMounth(value);
        }

        private string pageTitle;
        public string PageTitle
        {
            get => pageTitle;
            set => SetProperty(ref pageTitle, value);
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        private void DateSelected()
        {
            App.Current.MainPage = new ToDoPage(SelectedDate);
        }

        private void OpenToDoPage()
        {
            App.Current.MainPage = new ToDoPage(DateTime.Today);
        }
        private void SetMounth(int value)
        {
            SetProperty(ref mounth, value);
            var name = Culture.DateTimeFormat.GetMonthName(Mounth);
            PageTitle = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name);
        }
    }
}
