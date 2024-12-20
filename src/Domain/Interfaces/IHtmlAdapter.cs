namespace Cineplayers2Letterboxd.Domain.Interfaces;

public interface IHtmlAdapter
{
    void GetPage(string url);

    string GetHref(string link);

    List<string> GetTexts(string xPath);

    List<string> GetChildrenText(string xPath);

    string GetText(string xPath);

    string GetHtml(string xPath);
}
