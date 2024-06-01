using cineplayers_scraper.src.UseCases;

var username = "";

while (string.IsNullOrEmpty(username))
{
    Console.WriteLine("Por favor, insira seu nome de usuário do Cineplayers: ");
    username = Console.ReadLine() ?? "";
}

var exportToLetterboxd = new ExportToLetterboxd() { Username = username };

exportToLetterboxd.Handle();