using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Diary
{
    public partial class App : Application
    {

        public App()
        {
            var isNetworkAvailable = System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
            LocalUserInfoService.GetUserInfo();
            if(LocalUserInfoService.IsUserGuest)
            {
                DependencyService.Register<INotesDispatcher, LocalNotesDispatcherService>();
            }
            else if(isNetworkAvailable)
            {
                DependencyService.Register<INotesDispatcher, OnlineNoteDispatcherService>();
            }
            else
            {
                DependencyService.Register<INotesDispatcher, LocalNotesDispatcherService>();
            }
            InitializeComponent();

            if (LocalUserInfoService.IsUserAuth)
            {
                if (isNetworkAvailable)
                {
                    SaveToLocalNotes(DateTime.Today);
                    SaveToLocalNotes(DateTime.Today.AddDays(1));
                }
                MainPage = new NavigationPage(new PlanerPage());
            }
            else if (LocalUserInfoService.IsUserGuest)
            {
                MainPage = new NavigationPage(new PlanerPage());
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            NavigationDispatcher.Instance.Initialize(MainPage.Navigation);

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async Task SaveToLocalNotes(DateTime date)
        {
            var notes = await DependencyService.Get<INotesDispatcher>().GetNotes(date);
            string filePath = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/" + date.ToShortDateString().Replace('.', '-') + ".json";
            var json = JsonSerializer.Serialize(notes);
            File.WriteAllText(filePath, json);
        }
    }
}
