using Cineplayers2Letterboxd.Domain.Interfaces;
using Cineplayers2Letterboxd.Infrastructure.Extensions;
using HtmlAgilityPack;
using System.Text;

namespace Cineplayers2Letterboxd.Infrastructure.Adapters;

public class HtmlAgilityPackAdapter : IHtmlAdapter
{
    private readonly HtmlWeb Web;

    private HtmlDocument HtmlDocument;

    public HtmlAgilityPackAdapter()
    {
        Web = new HtmlWeb()
        {
            UsingCache = false,
            UsingCacheIfExists = false,
            OverrideEncoding = Encoding.UTF8
        };
    }

    public List<string> GetTexts(string xPath) => Nodes(xPath)?.Select(el => el.InnerText.Decode().Trim()).ToList() ?? [];

    public List<string> GetChildrenText(string xPath) => SingleNode(xPath)?.ChildNodes.Select(el => el.InnerText.Decode().Trim()).ToList();

    public string GetText(string xPath) => SingleNode(xPath)?.InnerText.Decode().Trim();

    public string GetHtml(string xPath) => SingleNode(xPath)?.InnerHtml;

    public string GetHref(string link) => SingleNode(link)?.Attributes["href"]?.Value;

    public void GetPage(string url) => HtmlDocument = Web.Load(url);

    private HtmlNode SingleNode(string xPath) => HtmlDocument.DocumentNode.SelectSingleNode(xPath);

    private HtmlNodeCollection Nodes(string xPath) => HtmlDocument.DocumentNode.SelectNodes(xPath);
}
