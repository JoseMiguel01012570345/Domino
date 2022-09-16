using System.Collections.Generic;
namespace Logic;
public static class Judgers
{
    static int TurnsToPlay = -1;
    static int PlayerOrder = 0;
    static int iterator = 0;
    static int CountDown = 0;
    //Parametros
    //---- ultima jugada
    //---- jugadores del juego
    public static int JudgerRuner(Dictionary<string,object> Params)//juega mientras pueda
    {
        if (((DominoMovement<int>)Params["LastMovement"]).Pieces != null)
        {
            return PlayerOrder;
        }
        else
        {
            if (PlayerOrder == ((IDominoPlayer<int>[])Params["Players"]).Length - 1) return PlayerOrder = 0;

            return PlayerOrder++;
        }
    }
    //Parametros
    //---- jugadores del juego
    //---- pases de cada jugador
    public static int JudgerJose(Dictionary<string,object> Params)//el int devuelto es el jugador que le toca jugar
    {
        if (TurnsToPlay == -1)
        {
            if (iterator == 0) iterator = ((IDominoPlayer<int>[])Params["Players"]).Length;
            iterator--;
            TurnsToPlay = Max<int>(Params) - ((Dictionary<IDominoPlayer<int>,int>)Params["JumpsByPlayer"])[((IDominoPlayer<int>[])Params["Players"])[iterator]];
        }
        if (TurnsToPlay > -1)
            TurnsToPlay--;

        return ((IDominoPlayer<int>[])Params["Players"]).ToList().IndexOf(((IDominoPlayer<int>[])Params["Players"])[iterator]);//juega seguido la diferencia entre los turnos pasados del jugador y la mayor cantidad de turnos pasados
    }
    //Parametros
    //---- pases por jugadores
    //---- jugadores del juego
    static int Max<T>(Dictionary<string,object> Params)
    {
        int best = 0;
        for (int i = 0; i < ((IDominoPlayer<T>[])Params["Players"]).Length; i++)
        {
            if (best < ((Dictionary<IDominoPlayer<T>,int>)Params["JumpsByPlayer"])[((IDominoPlayer<T>[])Params["Players"])[i]]) best = ((Dictionary<IDominoPlayer<T>,int>)Params["JumpsByPlayer"])[((IDominoPlayer<T>[])Params["Players"])[i]];
        }
        return best;
    }
    //Parametros
    //---- indice del jugador que le toca jugar
    //---- jugadores del juego
    public static int ClassicNextPlayer(Dictionary<string,object> Params)
    {
        int player = (int)Params["Player"];
        player++;
        player %= ((IDominoPlayer<int>[])Params["Players"]).Length;
        return player;
    }
}