using Diary.Models;
using Diary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNoteTimePage : ContentPage
    {
        public EditNoteTimePage(Note newNote)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new EditNoteTimeViewModel(newNote);
        }
    }
}