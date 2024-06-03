using System.Globalization;

namespace cineplayers2letterboxd.src.Entities;

public class LetterboxdMovie
{
    public string Title { get; set; }

    public string Year { get; set; }

    public string Directors { get; set; }

    public string Rating10 { get; private set; }

    public string Review { get; set; }

    public string WatchedDate { get; private set; }

    public LetterboxdMovie(string title, string year, string directors, string rating10, string review, string watchedDate)
    {
        Title = title;
        Year = year;
        Directors = directors;
        Rating10 = rating10;
        Review = review;
        WatchedDate = watchedDate;
    }

    public override string ToString() => $"... {Title} ({Directors}, {Year})";
}
