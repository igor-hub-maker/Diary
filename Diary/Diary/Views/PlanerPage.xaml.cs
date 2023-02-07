using Diary.Services;
using Diary.ViewModels;
using Xamarin.Forms;

namespace Diary.Views
{
    public partial class PlanerPage : ContentPage
    {
        public PlanerPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new PlanerViewModel();
        }
    }
}