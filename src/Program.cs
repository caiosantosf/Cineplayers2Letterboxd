using cineplayers2letterboxd.src.UseCases;

var username = "";

while (string.IsNullOrEmpty(username))
{
    Console.WriteLine("Bem-vindo ao Cineplayers2Letterboxd!");
    Console.WriteLine("");
    Console.WriteLine("Aqui você pode gerar o arquivo CSV para migrar seus filmes. A página para importa-los é a: https://letterboxd.com/import");
    Console.WriteLine("");
    Console.WriteLine("Caso sua lista de filmes seja maior que 1900, a exportação quebrará em mais arquivos CSV devido a limitação do Letterboxd");
    Console.WriteLine("");
    Console.WriteLine("Por favor, insira seu nome de usuário do Cineplayers: ");
    username = Console.ReadLine() ?? "";
}

new ExportToLetterboxd() { Username = username }.Handle();