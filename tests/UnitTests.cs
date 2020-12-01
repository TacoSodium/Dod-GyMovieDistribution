using System.Collections.Generic;
using Xunit;
using API.models;

namespace tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData(4, 324668, "Jason Bourne", 2016, 123)]
        [InlineData(2, 1572, "Die Hard: With a Vengeance", 1995, 131)]
        [InlineData(1, 869, "Planet of the Apes", 2001, 119)]
        [InlineData(7, 152532, "Dallas Buyers Club", 2013, 117)]
        [InlineData(1, 58595, "Snow White and the Huntsman", 2012, 127)]
        public void NumActorsTest(int answer, int movieno, string title, int relyear, int runtime)
        {
            Movie movie = new Movie(movieno, title, relyear, runtime);

            Actor a1 = new Actor(7, "Andrew", "Stanton");
            Actor a2 = new Actor(8, "Lee", "Unkrich");
            Actor a3 = new Actor(10, "Bob", "Peterson");
            Actor a4 = new Actor(19, "Allison", "Janney");
            Actor a5 = new Actor(31, "Tom", "Hanks");

            if (movie.RelYear < 2000)
            {
                movie.Actors.Add(a1);
                movie.Actors.Add(a2);
            }
            else if (movie.RelYear < 2015)
            {
                movie.Actors.Add(a5);
            }
            else
            {
                movie.Actors.Add(a3);
                movie.Actors.Add(a4);
                movie.Actors.Add(a5);
                movie.Actors.Add(a1);
            }
            

            Assert.Equal(answer, movie.Actors.Count);
        }

        [Theory]
        [InlineData(4, 324668, "Jason Bourne", 2016, 123)]
        [InlineData(20, 1572, "Die Hard: With a Vengeance", 1995, 131)]
        [InlineData(12, 869, "Planet of the Apes", 2001, 119)]
        [InlineData(7, 152532, "Dallas Buyers Club", 2013, 117)]
        [InlineData(8, 58595, "Snow White and the Huntsman", 2012, 127)]
        public void GetAgeTest(int answer, int movieno, string title, int relyear, int runtime)
        {
            Movie movie = new Movie(movieno, title, relyear, runtime);

            Assert.Equal(answer, movie.GetAge());
        }
    }
}
