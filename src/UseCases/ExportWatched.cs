using Cineplayers2Letterboxd.Domain.Entities;
using Cp = Cineplayers2Letterboxd.Domain.Paths.CineplayersPaths;
using Cineplayers2Letterboxd.Infrastructure.Extensions;
using Cineplayers2Letterboxd.Domain.Interfaces;
using Cineplayers2Letterboxd.Infrastructure.Adapters; 

namespace Cineplayers2Letterboxd.UseCases;

public class ExportWatched
{
    public class Command(string username, IHtmlAdapter html) : ICommand
    {
        public string Username { get; set; } = username;
        public IHtmlAdapter Html { get; set; } = html;
    }

    public class Handler() : IHandler<Command>
    {
        public Task Handle(Command command, CancellationToken cancellationToken)
        {
            Cp.Username = command.Username;

            var html = command.Html;

            html.GetPage(Cp.FullPath(command.Username));

            var userPathWithId = html.GetHref(Cp.ShortLink);
            var userMoviesPath = Cp.UserMoviesPath(userPathWithId);

            List <LetterboxdMovie> movies = [];
            var tasks = new List<Task>();

            var page = 0;

            while (page >= 0)
            {
                var userMoviesPaginatedPath = Cp.PaginatedPath(userMoviesPath, page);

                Console.WriteLine($"Página {page}");

                html.GetPage(userMoviesPaginatedPath);

                var moviesUrls = html.GetTexts(Cp.MovieLink).Select(el => Cp.FullPath(html.GetHref(el))).ToList();

                if (moviesUrls.Count == 0)
                {
                    page = -1;
                    continue;
                }

                var ratings = html.GetTexts(Cp.RatingDiv);

                tasks.AddRange(moviesUrls.Select((_, index) =>
                {
                    var currentIndex = index;
                    return Task.Run(() => movies.Add(GetMovie(moviesUrls[currentIndex], ratings[currentIndex], html)));
                }));

                Task.WaitAll([.. tasks], cancellationToken);

                page++;
            }

            if (movies.Count == 0)
                Console.WriteLine("Algo ocorreu errado, não encontramos seus filmes, verifique seu nome de usuário e seu perfil no Cineplayers.");

            var count = 0;

            for (int i = 0; i < movies.Count; i += 1900)
            {
                var sublist = movies.GetRange(i, Math.Min(i + 1900, movies.Count) - i);

                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"movies{count++}.csv");

                if (File.Exists(path))
                    File.WriteAllText(path, string.Empty);

                File.WriteAllText(path, sublist.ToCsv());
            }

            Console.WriteLine("CSV gerado com sucesso na mesma pasta deste executável!");

            return Task.CompletedTask;
        }

        private LetterboxdMovie GetMovie(string path, string rating, IHtmlAdapter html)
        {
            try
            {
                html.GetPage(path);

                var titleWithYear = html.GetText(Cp.TitleWithYearDiv);
                var titleYearDelimiter = titleWithYear.LastIndexOf(',');
                var title = titleWithYear[..titleYearDelimiter].Replace("(", "");
                var year = titleWithYear[(titleYearDelimiter + 2)..].Replace(")", "");
                var directors = html.GetText(Cp.DirectorsLink);
                var movieReviewPaths = html.GetHref(Cp.ShortLink).Replace("node", "lupas");

                var review = "";
                var watchedDate = "";
                var previousReviewBoard = "";

                for (var pageReviews = 0; pageReviews <= 999; pageReviews++)
                {
                    var movieReviewsPaginatedPath = Cp.PaginatedPath(movieReviewPaths, pageReviews);

                    html.GetPage(movieReviewsPaginatedPath);

                    var reviewBoard = html.GetHtml(Cp.ReviewsList);

                    if (reviewBoard == previousReviewBoard)
                        break;

                    previousReviewBoard = reviewBoard;

                    review = html.GetText(Cp.UserReview()) ?? "";

                    if (string.IsNullOrEmpty(review))
                        continue;

                    watchedDate = html.GetChildrenText(Cp.UserReviewMetadata()).Single(el => el.Contains("Em"));

                    break;
                }

                var cineplayersMovie = new CineplayersMovie(title, year, directors, rating, review, watchedDate);
                var letterboxdMovie = new LetterboxdMovie(cineplayersMovie);

                Console.WriteLine(letterboxdMovie);

                return letterboxdMovie;
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERRO!!!!: {path} {e}");
                return null;
            }
        }
    }
}
