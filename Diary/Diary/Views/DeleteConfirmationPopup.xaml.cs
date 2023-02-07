using System;
using Xamarin.CommunityToolkit.UI.Views;

namespace Diary.Views
{
    public partial class DeleteConfirmationPopup : Popup
    {
        public DeleteConfirmationPopup()
        {
            InitializeComponent();
        }

        private void Button_Yes(object sender, EventArgs e)
        {
            Dismiss(true);
        }

        private void Button_No(object sender, EventArgs e)
        {
            Dismiss(false);
        }
    }
}