using System;
using System.Collections.Generic;
using System.Text;

namespace MoodTracker.Database
{
    internal class Day
    {
        public int id;
        public string date;
        public int mood;
        public string note;

        public Day(string date, int mood, string note, int id = 0)
        {
            this.date = date;
            this.mood = mood;
            this.note = note;
            this.id = id;
            //id = (int.Parse(split[2]) * 385) + (int.Parse(split[1]) * 32) + int.Parse(split[0]);
        }
    }
}
