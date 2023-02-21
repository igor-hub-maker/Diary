using Diary.ViewModels;
using System;

using Xamarin.Forms;

namespace Diary.Views
{
    public partial class ToDoPage : ContentPage
    {
        public ToDoPage(DateTime date)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new ToDoViewModel(date);
        }
    }
}