using System;
using System.Collections.Generic;

namespace API.models
{
    public class Movie
    {
        public int MovieNo { get; set; }
        public string Title { get; set; }
        public int RelYear { get; set; }
        public int RunTime { get; set; }
        public List<Actor> Actors { get; set; }

        // public Movie(int movieNo, string title, int relYear, int runTime)
        // {
        //     this.MovieNo = movieNo;
        //     this.Title = title;
        //     this.RelYear = relYear;
        //     this.RunTime = runTime;
        //     this.Actors = new List<Actor>();
        // }

        public Movie()
        {
            this.Actors = new List<Actor>();
        }

        public int NumActors()
        {
            return this.Actors.Count;
        }

        public int GetAge()
        {
            int movieAge;
            int currentYear = DateTime.Today.Year;

            movieAge = currentYear - this.RelYear;

            return movieAge;
        }
    }
}
