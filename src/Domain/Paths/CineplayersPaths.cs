namespace Cineplayers2Letterboxd.Domain.Paths;

public static class CineplayersPaths
{
    public static string Username { get; set; }

    private static readonly string RootPath = "https://www.cineplayers.com";

    public static readonly string ShortLink = "//link[@rel=\"shortlink\"]";

    public static readonly string MovieLink = "//a[@class=\"title font-weight-bold\"]";
    
    public static readonly string RatingDiv = "//div[@class=\"rate-number\"]";

    public static readonly string TitleWithYearDiv = "//*[@id=\"secao-filme-webdoor\"]/div/div/div/div[1]/div[1]";

    public static readonly string DirectorsLink = "//*[@id=\"main\"]/div[2]/div/article/div[1]/div[1]/div/div/div/section/div[2]/dl/dd[1]/a";

    public static readonly string ReviewsList = "//*[@id=\"main\"]/div[2]/div/article/div/div[2]/div/div/section/ul";

    public static string FullPath(string path) 
        => $"{RootPath}/{path}";

    public static string UserMoviesPath(string userPathWithId) 
        => $"{userPathWithId}/notas?type=movie";

    public static string PaginatedPath(string path, int page) 
        => $"{path}&page={page}";

    public static string UserReview() 
        => $"//a[@href='/{Username}' and @class='text-highlight text-color-users-dark']/../preceding-sibling::div[1]/p[1]";

    public static string UserReviewMetadata() 
        => $"//a[@href='/{Username}' and @class='text-highlight text-color-users-dark']/..";
}
