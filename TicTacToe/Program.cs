using System.Text.Json;
using System.Text.RegularExpressions;

namespace TicTacToe;

class Program
{
    static async Task Main()
    {
        using var client = new HttpClient();
        {
            var r = await client.GetAsync(
                      "http://localhost:8080/jugadors"
            );
            Console.WriteLine(r.StatusCode);
            r.EnsureSuccessStatusCode();

            var jsonString = await r.Content.ReadAsStringAsync();
            var arrayStrings = JsonSerializer.Deserialize<string[]>(jsonString);

            List<Jugador> jugadors = new List<Jugador>();

            foreach (var i in arrayStrings ?? Array.Empty<string>())
            {
                Program.CrearJugadors(i, ref jugadors);
            }
            foreach (var jug in jugadors)
            {
                Console.WriteLine(jug.pais);
            }
        }

    }
    public static void CrearJugadors(string comentari, ref List<Jugador> jugadors)
    {
        string patroPais = @"representa\w*\s\w*\s(?<pais>[\w-]*)";
        Match match = Regex.Match(comentari, patroPais);
        Jugador j = new Jugador("jeje",match.Groups["pais"].ToString());
        jugadors.Add(j);
    }
}