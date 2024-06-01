using System;
using System.Globalization;
using System.Net;
using System.Text;

namespace cineplayers_scraper;

public class Movie
{
    public string Title { get; set; }

    public string Year { get; set; }

    public string Directors { get; set; }

    public string Rating10 { get; private set; }

    public string Review { get; set; }

    public string WatchedDate { get; private set; }

    public Movie(string title, string year, string directors, string rating10, string review, string watchedDate)
    {
        Title = WebUtility.HtmlDecode(title);
        Year = year;
        Directors = WebUtility.HtmlDecode(directors);
        Rating10 = Math.Truncate(float.Parse(rating10, CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
        Review = WebUtility.HtmlDecode(review).Replace(",", "");
        WatchedDate = ConvertDate(watchedDate);
    }

    private static string ConvertDate(string dateReceived)
    {
        string format = "d 'de' MMMM 'de' yyyy";

        if (DateTime.TryParseExact(dateReceived, format, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime date))
            return date.ToString("yyyy-MM-dd");

        return "";
    }

    public override string ToString() => $"... {Title} ({Directors}, {Year})";
}
