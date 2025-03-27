namespace TicTacToe;

class Partida
{
    public int numero { get; set; }
    public string? jugador1 { get; set; }
    public string? jugador2 { get; set; }
    public string[]? tauler { get; set; }

    public string Guanyador(){
        for (int i = 0; i < 3; i++)
        {
            if(tauler[0][i] == 'O' && tauler[1][i] == 'O' && tauler[2][i] == 'O') return jugador1;
            if(tauler[0][i] == 'X' && tauler[1][i] == 'X' && tauler[2][i] == 'X') return jugador2;
            if(tauler[i] == "OOO") return jugador1;
            if(tauler[i] == "XXX") return jugador2;
        }
        if(tauler[0][0] == 'O' && tauler[1][1] == 'O' && tauler[2][2] == 'O') return jugador1;
        if(tauler[0][0] == 'X' && tauler[1][1] == 'X' && tauler[2][2] == 'X') return jugador2;
        return "";
    }
}