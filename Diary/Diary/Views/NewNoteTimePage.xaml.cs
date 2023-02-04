using Diary.Models;
using Diary.ViewModels;

using Xamarin.Forms;

namespace Diary.Views
{
    public partial class NewNoteTimePage : ContentPage
    {
        public NewNoteTimePage(Note task)
        {
            InitializeComponent();
            BindingContext = new NewNoteTimeViewModel(task);
        }
    }
}