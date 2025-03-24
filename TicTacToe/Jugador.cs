namespace TicTacToe;

class Jugador
{
    public int partidesGuanyades;
    public string? nomComplet;
    public string? pais;

    public Jugador(string nom, string paisJugador)
    {
        nomComplet = nom;
        pais = paisJugador;
    }
}