using Diary.ViewModels;
using Xamarin.Forms;

namespace Diary.Views
{
    public partial class PlanerPage : ContentPage
    {
        public PlanerPage()
        {
            InitializeComponent();
            BindingContext = new PlanerViewModel();
        }
    }
}