using Diary.Services;
using Diary.Services.Interfaces;
using Diary.Views;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class PlanerViewModel : BaseViewModel
    {
        public PlanerViewModel()
        {
            Culture = CultureInfo.GetCultureInfoByIetfLanguageTag("en-GB");
            Mounth = DateTime.Now.Month;
            SelectedDateCommand = new AsyncCommand(DateSelected);
            OpenToDoPageCommand = new AsyncCommand(OpenToDoPage);
            AppearingCommand = new AsyncCommand(Appearing);
        }

        public ICommand SelectedDateCommand { get; }
        public ICommand OpenToDoPageCommand { get; }
        public ICommand AppearingCommand { get; }

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

        private async Task DateSelected()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new ToDoPage(SelectedDate), true);
        }

        private async Task OpenToDoPage()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new ToDoPage(DateTime.Today), true);
        }
        private void SetMounth(int value)
        {
            SetProperty(ref mounth, value);
            var name = Culture.DateTimeFormat.GetMonthName(Mounth);
            PageTitle = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(name);
        }
        private async Task Appearing()
        {
            TodayNotesCount = await DependencyService.Get<INotesDispatcher>().GetNotesCount(DateTime.Today);
        }
    }
}
