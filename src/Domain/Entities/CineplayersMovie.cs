namespace Cineplayers2Letterboxd.Domain.Entities;

public class CineplayersMovie : Movie
{
    public string Rating { get; private set; }

    public CineplayersMovie(string title, string year, string directors, string rating, string review, string watchedDate)
    {
        Title = title;
        Year = year;
        Directors = directors;
        Rating = rating;
        Review = review;
        WatchedDate = watchedDate;
    }
}
