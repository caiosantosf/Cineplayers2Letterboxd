using Cineplayers2Letterboxd.Domain.Interfaces;
using Cineplayers2Letterboxd.UseCases;

namespace Cineplayers2Letterboxd.Presentation;

public static class ConsoleMenu
{
    public async static Task Run(IMediatorAdapter mediator, IHtmlAdapter html)
    {
        var username = "";

        Console.WriteLine("Bem-vindo ao Cineplayers2Letterboxd!");
        Console.WriteLine("");
        Console.WriteLine("Aqui você pode gerar o arquivo CSV para migrar seus filmes. A página para importa-los é a: https://letterboxd.com/import");
        Console.WriteLine("");
        Console.WriteLine("Se seus filmes forem mais de 1900, a exportação quebrará em mais CSVs devido a limitação do Letterboxd");

        while (string.IsNullOrEmpty(username))
        {
            Console.WriteLine("");
            Console.WriteLine("Por favor, insira seu nome de usuário do Cineplayers: ");
            username = Console.ReadLine() ?? "";
        }

        var exit = false;
        while (!exit)
        {
            var choice = 0;

            while (choice == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Para exportar seus filmes assistidos digite 1: ");
                Console.WriteLine("Para exportar seus filmes sua watchlist (\"quero assistir\") digite 2: ");

                choice = Menu(2);
            }

            if (choice == 1)
                await mediator.Send(new ExportWatched.Command(username, html));
            if (choice == 2)
                await mediator.Send(new ExportWatchlist.Command(username, html));

            while (choice == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Para encerrar digite 1: ");
                Console.WriteLine("Para voltar ao menu de opções digite 2: ");

                choice = Menu(2);
            }

            if (choice == 1)
                exit = true;

            if (choice == 2)
                exit = false;
        }

        Environment.Exit(0);

        static int Menu(int options)
        {
            bool success = int.TryParse(Console.ReadLine(), out int choice);

            if (!success || choice < 0 || choice > options)
                choice = 0;

            return choice;
        }
    }
}
