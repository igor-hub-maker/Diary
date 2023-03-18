using Diary.Models;
using Diary.Services;
using Diary.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public WelcomeViewModel()
        {
            LoginCommand = new AsyncCommand(Login);
            RegisterCommand = new AsyncCommand(Register);
            ContinueAsGuestCommand = new Command(ContinueAsGuest);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ContinueAsGuestCommand { get; }

        private async Task Login()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new LoginPage());
        }
        private async Task Register()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new RegisterPage());
        }
        private void ContinueAsGuest()
        {
            LocalUserInfoService.SaveUserInfo(new User() { Login = "", Id = -1, Password = "" });
            App.Current.MainPage = new NavigationPage(new PlanerPage());
            NavigationDispatcher.Instance.Initialize(App.Current.MainPage.Navigation);
        }
    }
}
