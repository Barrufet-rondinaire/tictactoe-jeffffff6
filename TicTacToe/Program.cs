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

            string urlPartida = "http://localhost:8080/partida/";
            Partida partida = new Partida();

            for (int i = 0; i < 10000; i++)
            {
                string partidaNum = string.Concat(urlPartida, i.ToString());
                r = await client.GetAsync(
                      partidaNum
                );

                partida = JsonSerializer.Deserialize<Partida>(await r.Content.ReadAsStringAsync());
                Console.WriteLine(partidaNum);

            }
            

        }

    }
    public static void CrearJugadors(string comentari, ref List<Jugador> jugadors)
    {
        string patroNom = @"participant\s(?<nomComplet>[A-Za-z]*\s[A-Za-z-']*)";
        string patroPais = @"representa\w*\s\w*\s(?<pais>[\w-]*)";
        string desqualificada = "desqualificada";
        Match matchNom = Regex.Match(comentari, patroNom);
        Match matchPais = Regex.Match(comentari, patroPais);
        Match matchDesqualificada = Regex.Match(comentari, desqualificada);
        Jugador j = new Jugador(matchNom.Groups["nom"].ToString(),matchPais.Groups["pais"].ToString(), matchDesqualificada.Success);
        jugadors.Add(j);
    }
}