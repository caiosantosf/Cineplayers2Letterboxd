namespace Cineplayers2Letterboxd.Domain.Entities;

public abstract class Movie
{
    public string Title { get; set; }

    public string Year { get; set; }

    public string Directors { get; set; }

    public string Review { get; set; }

    public string WatchedDate { get; set; }

    public override string ToString() => $"... {Title} ({Directors}, {Year})";
}
