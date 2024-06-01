using cineplayers_scraper.src.Entities;
using cineplayers_scraper.src.Extensions;
using HtmlAgilityPack;
using System.Text;

namespace cineplayers_scraper.src.UseCases;

public class ExportToLetterboxd
{
    private HtmlWeb Web;
    private List<LetterboxdMovie> Movies;
    public string Username { get; set; }

    public void Handle()
    {
        Web = new HtmlWeb()
        {
            UsingCache = false,
            UsingCacheIfExists = false,
            OverrideEncoding = Encoding.UTF8
        };

        var rootUrl = "https://www.cineplayers.com";
        var userUrl = $"{rootUrl}/{Username}";

        var doc = Web.Load(userUrl);

        var userUrlWithId = doc.DocumentNode
            .SelectSingleNode("//link[@rel=\"shortlink\"]/@href").Attributes["href"].Value;

        var userMoviesUrl = $"{userUrlWithId}/notas?type=movie";

        Movies = [];
        var tasks = new List<Task>();

        for (var page = 0; ; page++)
        {
            var userMoviesPaginatedUrl = $"{userMoviesUrl}&page={page}";

            Console.WriteLine($"Página {page}");

            doc = Web.Load(userMoviesPaginatedUrl);

            var moviesUrls = doc.DocumentNode?
                .SelectNodes("//a[@class=\"title font-weight-bold\"]")?
                .Select(node => $"{rootUrl}{node.Attributes["href"].Value}").ToList();

            if (moviesUrls == null || moviesUrls.Count == 0)
                break;

            var ratings = doc.DocumentNode
                .SelectNodes("//div[@class=\"rate-number\"]")
                .Select(node => node.InnerText.Replace(",", ".")).ToList();

            tasks.AddRange(moviesUrls.Select((_, index) =>
            {
                var currentIndex = index;
                return Task.Run(() => GetMovie(moviesUrls[currentIndex], ratings[currentIndex]));
            }));

            Task.WaitAll([.. tasks]);
        }

        if (Movies == null || Movies.Count == 0)
        {
            Console.WriteLine("Algo ocorreu errado, não encontramos seus filmes, verifique seu nome de usuário e seu perfil no Cineplayers.");
            Console.WriteLine("Digite qualquer tecla para encerrar");
            Console.ReadKey(true);
            Environment.Exit(1);
        }

        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "movies.csv");

        if (File.Exists(filePath))
            File.WriteAllText(filePath, string.Empty);

        File.WriteAllText(filePath, Movies.ToCsv());

        Console.WriteLine("CSV gerado com sucesso na mesma pasta deste executável!");
        Console.WriteLine("Digite qualquer tecla para encerrar");
        Console.ReadKey(true);
        Environment.Exit(0);
    }

    private void GetMovie(string url, string rating)
    {
        try
        {
            var doc = Web.Load(url);

            var titleWithYear = doc.DocumentNode
                .SelectSingleNode("//*[@id=\"secao-filme-webdoor\"]/div/div/div/div[1]/div[1]").InnerText;

            var titleYearDelimiter = titleWithYear.LastIndexOf(',');

            var title = titleWithYear[..titleYearDelimiter].Replace("(", "");

            if (title.Split(",").Length > 1)
                title = $"{title.Split(",")[1].Trim()} {title.Split(",")[0].Trim()}";

            var year = titleWithYear[(titleYearDelimiter + 2)..].Replace(")", "");

            var directors = doc.DocumentNode
                .SelectSingleNode("//*[@id=\"main\"]/div[2]/div/article/div[1]/div[1]/div/div/div/section/div[2]/dl/dd[1]/a")
                .InnerText;

            var movieReviewsUrl = doc.DocumentNode
                .SelectSingleNode("//link[@rel=\"shortlink\"]/@href").Attributes["href"].Value.Replace("node", "lupas");

            var review = "";
            var watchedDate = "";
            var previousReviewBoard = "";

            for (var pageReviews = 0; pageReviews <= 999; pageReviews++)
            {
                var movieReviewsPaginatedUrl = $"{movieReviewsUrl}?page={pageReviews}";

                doc = Web.Load(movieReviewsPaginatedUrl);

                var reviewBoard = doc.DocumentNode
                    .SelectSingleNode("//*[@id=\"main\"]/div[2]/div/article/div/div[2]/div/div/section/ul")?.InnerHtml;

                if (reviewBoard == previousReviewBoard)
                    break;

                previousReviewBoard = reviewBoard;

                review = doc.DocumentNode
                    .SelectSingleNode($"//a[@href='/{Username}' and @class='text-highlight text-color-users-dark']/../preceding-sibling::div[1]/p[1]")?
                    .InnerText.Trim();

                if (review == null)
                    continue;

                watchedDate = doc.DocumentNode
                    .SelectSingleNode($"//a[@href='/{Username}' and @class='text-highlight text-color-users-dark']/..")?
                    .ChildNodes.Single(x => x.InnerText.Contains("Em")).InnerText.Trim();

                break;
            }

            var movie = new LetterboxdMovie(title, year, directors, rating, review, watchedDate);

            Console.WriteLine(movie);

            Movies.Add(movie);
        }
        catch (Exception e)
        {
            Console.WriteLine($"!!!!!!!! ERRO: {url} {e}");
        }
    }
}
