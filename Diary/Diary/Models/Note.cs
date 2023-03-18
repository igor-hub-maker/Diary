using System;

namespace Diary.Models
{
    public class Note
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Time { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
