using Diary.Models;
using Diary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNoteInfoPage : ContentPage
    {
        public EditNoteInfoPage(Note note)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new EditNoteInfoViewModel(note);
        }
    }
}