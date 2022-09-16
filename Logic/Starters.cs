using System.Collections.Generic;
using System.IO.Pipes;
namespace Logic;
public static class Starters
{
    //Parametros:
    //---- los jugadores
    //---- las fichas del juego
    //---- las fichas por jugador
    //---- maximo numero de fichas por jugador
    //---- el metodo que reparte las fichas
    public static int ClassicDoubleSixStarter(Dictionary<string,object> Params)
    {
        try
        {
            int result = 0;
            //((Action<Dictionary<string,object>>)Params["SorterPieces"]).Invoke(Params);
            for(int i = 0; i < ((IDominoPlayer<int>[])Params["Players"]).Length; i++)
            {
                bool found = false;
                foreach(ClassicDominoPiece piece in ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["Players"])[i]])
                {
                    if(piece.Left == piece.Right && piece.Right == ((int)Params["MaxNumberOfPieces"]) - 1)
                    {
                        found = true;
                        result = i;
                        break;
                    }
                }
                if(found)
                    break;
            }
            return result;
        }
        catch(Exception e)
        {
            return -1;
        }
    }
    //Parametros:
    //---- los jugadores
    //---- las fichas del juego
    //---- las fichas por jugador
    //---- maximo numero de fichas por jugador
    //---- el metodo que reparte las fichas
    public static int ClassicDoubleNineStarter(Dictionary<string,object> Params)
    {
        //((Action<Dictionary<string,object>>)Params["SorterPieces"]).Invoke(Params);
        int[] choices = new int[((IDominoPlayer<int>[])Params["Players"]).Length];
        Random random = new Random();
        for(int i = 0; i < choices.Length; i++)
            choices[i] = random.Next((((int)Params["MaxNumberOfPieces"]) - 1) * 2);
        int player = 0;
        for(int i = 0; i < choices.Length; i++)
            if(choices[i] > choices[player])
                player = i;
        return player;
    }
}