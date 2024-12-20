using System.Reflection;
using System.Text;

namespace Cineplayers2Letterboxd.Infrastructure.Extensions;

public static class ListExtensions
{
    public static string ToCsv<T>(this List<T> itens)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var csvBuilder = new StringBuilder();

        csvBuilder.AppendLine(string.Join(",", properties.Select(prop => prop.Name)));

        foreach (var item in itens)
        {
            var values = properties.Select(prop => prop.GetValue(item)?.ToString() ?? "").ToArray();
            csvBuilder.AppendLine(string.Join(",", values));
        }

        return csvBuilder.ToString();
    }
}
