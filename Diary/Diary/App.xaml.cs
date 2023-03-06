using Diary.Models;
using Diary.Services;
using Diary.Services.Interfaces;
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
            LocalUserInfoService.GetUserInfo();
            DependencyService.RegisterSingleton<INotesDispatcher>(new NotesDispatcher());
            InitializeComponent();
            MainPage = new NavigationPage( new StartPage());
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
       
    }
}
