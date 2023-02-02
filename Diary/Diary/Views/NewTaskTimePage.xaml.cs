using Diary.Models;
using Diary.ViewModels;

using Xamarin.Forms;

namespace Diary.Views
{
    public partial class NewTaskTimePage : ContentPage
    {
        public NewTaskTimePage(Note task)
        {
            InitializeComponent();
            BindingContext = new NewTaskTimeViewModel(task);
        }
    }
}