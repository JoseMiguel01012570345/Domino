using System.Collections.Generic;
namespace Logic;
public static class GameOverers
{
    public static (string, int)[] parametros = { ("Cantidad de pases a perder", 2) };
    //Parametros
    //---- jugadores del juego
    //---- los pases que se ha dado a cada jugador
    public static bool GameOvererByJump(Dictionary<string,object> Params)
    {
        for (int i = 0; i < ((IDominoPlayer<int>[])Params["Players"]).Length; i++)
        {
            if (((Dictionary<IDominoPlayer<int>,int>)Params["JumpsByPlayer"])[((IDominoPlayer<int>[])Params["Players"])[i]] == parametros[0].Item2) 
                ((bool[])Params["PlayersPlaying"])[i] = true;
        }
        int count = 0;
        for(int i = 0; i < ((IDominoPlayer<int>[])Params["Players"]).Length; i++)
        {
            if(!((bool[])Params["PlayersPlaying"])[i])
                count++;
            if(count > 1)
                return false;
        }
        return true;
    }
    //Parametros
    //---- pases que se han dado en el juego
    //---- jugadores del juego
    //---- fichas de cada jugador
    public static bool ClassicGameOver(Dictionary<string,object> Params)
    {
        if(((int)Params["Jumps"]) == ((IDominoPlayer<int>[])Params["Players"]).Length)
            return true;
        foreach(var player in ((IDominoPlayer<int>[])Params["Players"]))
            if(((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[player].Count == 0)
                return true;
        return false;
    }
}