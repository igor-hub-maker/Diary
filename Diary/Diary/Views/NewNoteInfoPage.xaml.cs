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
            BindingContext = new NewNoteInfoViewModel(date);
        }
    }
}