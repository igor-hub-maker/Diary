using Diary.Models;
using Diary.Services;
using Diary.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class ToDoViewModel : BaseViewModel
    {
        public ToDoViewModel(DateTime date)
        {
            NewNoteCommand = new AsyncCommand(NewNote);
            OpenPlanerPageCommand = new AsyncCommand(OpenPlanerPage);
            NoteTapped = new AsyncCommand(NoteSelected);
            PageDate = date;
            PageTitle = $"{PageDate.Day} // {PageDate.Month}";
            WeekDay = PageDate.DayOfWeek.ToString();
            OnApperingCommand = new AsyncCommand(OnAppering);
        }

        private DateTime PageDate;

        public ICommand OpenPlanerPageCommand { get; }
        public ICommand NewNoteCommand { get; }
        public ICommand NoteTapped { get; }
        public ICommand OnApperingCommand { get; }

        private Note selectedNote;
        public Note SelectedNote
        {
            get => selectedNote;
            set => SetProperty(ref selectedNote, value);
        }
        private List<Note> notes;
        public List<Note> Notes
        {
            get => notes;
            set => SetProperty(ref notes, value);
        }

        private string pageTitle;
        public string PageTitle
        {
            get => pageTitle;
            set
            {
                pageTitle = value;
                OnPropertyChanged();
            }
        }
        private string weekDay;
        public string WeekDay
        {
            get => weekDay;
            set
            {
                weekDay = value;
                OnPropertyChanged();
            }
        }

        private async Task OnAppering()
        {
            Notes = await DependencyService.Get<INotesDispatcher>().GetNotes(PageDate);
        }
        public async Task NoteSelected()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new NoteDescriptionPage(SelectedNote));
        }
        public async Task OpenPlanerPage()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }

        public async Task NewNote()
        {
            await NavigationDispatcher.Instance.Navigation.PushAsync(new NewNoteInfoPage(PageDate));
        }
    }
}
