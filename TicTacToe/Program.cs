using System.Net.Http.Json;
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

            var arrayStrings = await r.Content.ReadFromJsonAsync<string[]>();

            List<Jugador> jugadors = new List<Jugador>();

            foreach (var i in arrayStrings ?? Array.Empty<string>())
            {
                Program.CrearJugadors(i, ref jugadors);
            }

            string urlPartida = "http://localhost:8080/partida";
            Partida partida = new Partida();

            for (int i = 1; i <= 10000; i++)
            {
                try
                {
                    r = await client.GetAsync($"{urlPartida}/{i}");
                    string json = await r.Content.ReadAsStringAsync();
                    Partida deserializedPartida = JsonSerializer.Deserialize<Partida>(json);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Request error en {i}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error en {i}: {ex.Message}");
                }

                


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
        Jugador j = new Jugador(matchNom.Groups["nom"].ToString(), matchPais.Groups["pais"].ToString(), matchDesqualificada.Success);
        jugadors.Add(j);
    }
}