using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Diary.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            ReturnCommand = new AsyncCommand(Return);
            EnterCommand = new AsyncCommand(Enter);
        }

        public ICommand ReturnCommand { get; }
        public ICommand EnterCommand { get; }

        private string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set => SetProperty(ref userPassword, value);
        }
        private string userName;
        public string UserLogin
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private async Task Return()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
        private async Task Enter()
        {
            var userAunth = new UserAunth();
            var emailAttribute = new EmailAddressAttribute();
            try
            {
                if (!emailAttribute.IsValid(UserLogin.TrimEnd()))
                {
                    await App.Current.MainPage.DisplayAlert("Ops...", "Email uncorrect", "Ok");
                    return;
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Ops...", "Email uncorrect", "Ok");
                return;
            }
            if (string.IsNullOrEmpty(UserPassword))
            {
                await App.Current.MainPage.DisplayAlert("Ops...", "Plese write password", "Ok");
                return;
            }
            var user = new User() { Login = UserLogin.TrimEnd(), Password = UserPassword };
            try
            {
                await userAunth.RegisterUser(user);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ops...", ex.Message, "Ok");
                return;
            }
            await NavigationDispatcher.Instance.Navigation.PopToRootAsync(true);
            await NavigationDispatcher.Instance.Navigation.PushAsync(new LoginPage());
        }
    }
}
