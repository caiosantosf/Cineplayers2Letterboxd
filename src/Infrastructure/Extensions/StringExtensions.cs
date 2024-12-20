using System.Globalization;
using System.Net;

namespace Cineplayers2Letterboxd.Infrastructure.Extensions;

public static class StringExtensions
{
    public static string ConvertCineplayersDateToLetterboxdDate(this string dateReceived)
    {
        string format = "d 'de' MMMM 'de' yyyy";

        if (DateTime.TryParseExact(dateReceived.Replace("Em ", ""), format,
            new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime date))
            return date.ToString("yyyy-MM-dd");

        return "";
    }

    public static string Decode(this string value) => WebUtility.HtmlDecode(value);
}