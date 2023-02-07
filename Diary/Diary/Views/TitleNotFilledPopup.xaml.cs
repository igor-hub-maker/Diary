using System;
using Xamarin.CommunityToolkit.UI.Views;

namespace Diary.Views
{
    public partial class TitleNotFilledPopup : Popup
    {
        public TitleNotFilledPopup()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}