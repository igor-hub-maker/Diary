﻿using Diary.Models;
using Diary.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Diary.ViewModels
{
    public class NewTaskInfoViewModel : BaseViewModel
    {
        private DateTime taskDate;

        public NewTaskInfoViewModel(DateTime date)
        {
            NewTaskContinueCommand = new Command(NewNoteContinue);
            OpenToDoPageCommand = new Command(OpenToDoPage);
            taskDate = date;
        }

        public ICommand OpenToDoPageCommand { protected set; get; }
        public ICommand NewTaskContinueCommand { protected set; get; }

        private string noteTitle;
        public string NoteTitle
        {
            get => noteTitle;
            set
            {
                noteTitle = value;
                OnPropertyChanged();
            }
        }

        private string noteDescription;
        public string NoteDescription
        {
            get => noteDescription;
            set
            {
                noteDescription = value;
                OnPropertyChanged();
            }
        }

        public void OpenToDoPage()
        {
            App.Current.MainPage = new ToDoPage(taskDate);
        }

        public void NewNoteContinue()
        {
            Note note = new Note();
            note.Date = taskDate;
            note.Title = NoteTitle;
            note.Description = NoteDescription;
            App.Current.MainPage = new NewTaskTimePage(note);
        }
    }
}
