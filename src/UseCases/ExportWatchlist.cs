using Cineplayers2Letterboxd.Domain.Entities;
using Cineplayers2Letterboxd.Domain.Interfaces;
using Cineplayers2Letterboxd.Infrastructure.Adapters;
using HtmlAgilityPack;

namespace Cineplayers2Letterboxd.UseCases;

public class ExportWatchlist
{
    private HtmlWeb Web;
    private List<LetterboxdMovie> Movies;

    public string Username { get; set; }

    public class Command(string username, IHtmlAdapter html) : ICommand
    {
        public string Username { get; set; } = username;
    }

    public void Handle()
    {

    }
}
