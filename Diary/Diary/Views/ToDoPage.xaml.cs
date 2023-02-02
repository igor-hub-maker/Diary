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
            BindingContext = new ToDoViewModel(date);
        }
    }
}