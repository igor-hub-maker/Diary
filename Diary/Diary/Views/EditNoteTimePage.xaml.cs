using Diary.Models;
using Diary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditNoteTimePage : ContentPage
    {
        public EditNoteTimePage(Note newNote, Note oldNote)
        {
            InitializeComponent();
            BindingContext = new EditNoteTimeViewModel(newNote, oldNote);
        }
    }
}