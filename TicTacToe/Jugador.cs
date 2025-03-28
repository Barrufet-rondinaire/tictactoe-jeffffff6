namespace TicTacToe;

class Jugador
{
    public int partidesGuanyades;
    public string? nomComplet;
    public string? pais;
    public bool desqualificat;

    public Jugador(string nom, string paisJugador, bool desqualificada)
    {
        nomComplet = nom;
        pais = paisJugador;
        desqualificat = desqualificada;
    }
}