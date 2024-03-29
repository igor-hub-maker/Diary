﻿using Diary.Models;
using Diary.Services;
using Diary.Services.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class NewNoteTimeViewModel : BaseViewModel
    {
        public NewNoteTimeViewModel(Note note)
        {
            SaveNoteCommand = new AsyncCommand(SaveNote);
            ReturnCommand = new AsyncCommand(Return);

            NewNote = note;
            SelectedTime = DateTime.Now.TimeOfDay;
            SelectedDate = NewNote.Date;
        }

        private Note NewNote;

        public ICommand SaveNoteCommand { get; }
        public ICommand ReturnCommand { get; }

        private TimeSpan selectedTime;
        public TimeSpan SelectedTime
        {
            get => selectedTime;
            set => SetProperty(ref selectedTime, value);
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        private async Task SaveNote()
        {
            NewNote.Date = SelectedDate.Add(SelectedTime);
            NewNote.Time = NewNote.Date.ToShortTimeString();
            await DependencyService.Get<INotesDispatcher>().SaveNote(NewNote);
            await NavigationDispatcher.Instance.Navigation.PopToRootAsync();
        }

        private async Task Return()
        {
            await NavigationDispatcher.Instance.Navigation.PopAsync();
        }
    }
}
