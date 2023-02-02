using Diary.ViewModels;
using System;

using Xamarin.Forms;

namespace Diary.Views
{
    public partial class NewTaskInfoPage : ContentPage
    {
        public NewTaskInfoPage(DateTime date)
        {
            InitializeComponent();
            BindingContext = new NewTaskInfoViewModel(date);
        }
    }
}