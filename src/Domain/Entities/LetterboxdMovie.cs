using Cineplayers2Letterboxd.Infrastructure.Extensions;

namespace Cineplayers2Letterboxd.Domain.Entities;

public class LetterboxdMovie : Movie
{
    public string Rating10 { get; set; }

    public LetterboxdMovie(CineplayersMovie cineplayersMovie)
    {
        var title = cineplayersMovie.Title;

        if (title.Split(",").Length > 1)
            Title = $"{title.Split(",")[1].Trim()} {title.Split(",")[0].Trim()}";
        else
            Title = title;

        Year = cineplayersMovie.Year;
        Directors = cineplayersMovie.Directors;
        Rating10 = cineplayersMovie.Rating.Split(',')[0];
        Review = $"\"{cineplayersMovie.Review} \"";
        WatchedDate = cineplayersMovie.WatchedDate.ConvertCineplayersDateToLetterboxdDate();
    }
}
