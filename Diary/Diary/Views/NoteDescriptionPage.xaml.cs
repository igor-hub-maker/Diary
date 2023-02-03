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
	public partial class NoteDescriptionPage : ContentPage
	{
		public NoteDescriptionPage (Note note)
		{
			InitializeComponent ();
			BindingContext = new NoteDescriptionViewModel(note);
		}
	}
}