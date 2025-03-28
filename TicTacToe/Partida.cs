namespace TicTacToe;

class Partida
{
    public int numero { get; set; }
    public string? jugador1 { get; set; }
    public string? jugador2 { get; set; }
    public string[]? tauler { get; set; }

    public string Guanyador(){
        if (tauler == null) return "";
        if (jugador1 == null) return "";//els punyeters nuls donant warnings
        if (jugador2 == null) return "";
        for (int i = 0; i < 3; i++)//verticals i horitzontals
        {
            if(tauler[0][i] == 'O' && tauler[1][i] == 'O' && tauler[2][i] == 'O') return jugador1;
            if(tauler[0][i] == 'X' && tauler[1][i] == 'X' && tauler[2][i] == 'X') return jugador2;
            if(tauler[i] == "OOO") return jugador1;
            if(tauler[i] == "XXX") return jugador2;
        }
        if (tauler[0][0] == tauler[1][1] && tauler[1][1] == tauler[2][2]){//diagonal
            if (tauler[0][0] == 'O') return jugador1;
            if (tauler[0][0] == 'X') return jugador2;
        }
        if (tauler[0][2] == tauler[1][1] && tauler[1][1] == tauler[2][0]){//diagonal inversa
            if (tauler[0][2] == 'O') return jugador1;
            if (tauler[0][2] == 'X') return jugador2;
        }
        return "";
    }
}