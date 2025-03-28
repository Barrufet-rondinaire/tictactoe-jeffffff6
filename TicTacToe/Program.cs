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
            r.EnsureSuccessStatusCode();

            var arrayStrings = await r.Content.ReadFromJsonAsync<string[]>();

            Dictionary<string,Jugador> jugadors = new Dictionary<string,Jugador>();

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
                    r.EnsureSuccessStatusCode();
                    partida = JsonSerializer.Deserialize<Partida>(await r.Content.ReadAsStringAsync());
                }
                catch (HttpRequestException ex){
                    Console.WriteLine($"error de http request en {i}: {ex.Message}");
                }catch (JsonException ex){
                    Console.WriteLine($"error de deserialización en {i}: {ex.Message}");
                }catch (Exception ex){
                    Console.WriteLine($"error en {i}: {ex.Message}");
                }

                if (partida != null && jugadors.ContainsKey(partida.Guanyador()) && !jugadors[partida.Guanyador()].desqualificat){
                    jugadors[partida.Guanyador()].partidesGuanyades++;
                };
            }
            foreach (var jugador in jugadors)
            {
                Console.WriteLine($"{jugador.Value.nomComplet} {jugador.Value.partidesGuanyades} - {jugador.Value.desqualificat}");
            }
        }
    }
    public static void CrearJugadors(string comentari, ref Dictionary<string,Jugador> jugadors)
    {
        string patroNom = @"participant\s(?<nomComplet>[A-Za-z]*\s[A-Za-z-']*)";
        string patroPais = @"representa\w*\s\w*\s(?<pais>[\w-]*)";
        string desqualificada = "desqualificada";
        Match matchNom = Regex.Match(comentari, patroNom);
        Match matchPais = Regex.Match(comentari, patroPais);
        Match matchDesqualificada = Regex.Match(comentari, desqualificada);
        string nomComplet = matchNom.Groups["nomComplet"].Value;
        string pais = matchPais.Groups["pais"].Value;
        
        if (!string.IsNullOrEmpty(nomComplet)){
            Jugador j = new Jugador(nomComplet, pais, matchDesqualificada.Success);
            if (!jugadors.ContainsKey(nomComplet)){
                jugadors.Add(nomComplet, j);
            }
        }
    }
}