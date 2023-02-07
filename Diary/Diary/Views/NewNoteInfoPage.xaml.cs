using Diary.ViewModels;
using System;

using Xamarin.Forms;

namespace Diary.Views
{
    public partial class NewNoteInfoPage : ContentPage
    {
        public NewNoteInfoPage(DateTime date)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new NewNoteInfoViewModel(date);
        }
    }
}