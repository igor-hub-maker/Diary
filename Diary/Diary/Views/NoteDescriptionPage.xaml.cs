using Diary.Models;
using Diary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteDescriptionPage : ContentPage
    {
        public NoteDescriptionPage(Note note)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new NoteDescriptionViewModel(note);
        }
    }
}