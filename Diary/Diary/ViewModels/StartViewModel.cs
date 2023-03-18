using Diary.Services;
using Diary.Services.Interfaces;
using Diary.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class StartViewModel: BaseViewModel
    {
        public StartViewModel()
        {
            OnApperingCommand = new Command(Appering);
        }

        public ICommand OnApperingCommand { get; }

        private void Appering()
        {
            if (LocalUserInfoService.IsUserAuth)
            {
                if (DependencyService.Get<INotesDispatcher>().IsInternetConectionEnable())
                {
                    LocalNotesWork();
                }
                App.Current.MainPage = new NavigationPage(new PlanerPage());
            }
            else if (LocalUserInfoService.IsUserGuest)
            {
                App.Current.MainPage = new NavigationPage(new PlanerPage());
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new WelcomePage());
            }
            NavigationDispatcher.Instance.Initialize(App.Current.MainPage.Navigation);
        }

        private async Task LocalNotesWork()
        {
            await DependencyService.Get<INotesDispatcher>().UploadFromLocalNotes();
            await DependencyService.Get<INotesDispatcher>().SaveToLocalNotes();
        }
    }
}
