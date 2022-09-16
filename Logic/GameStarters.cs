using System.Collections.Generic;
namespace Logic;
public static class GameStarters
{
    //Parametros
    //---- estado inicial del juego
    //---- fichas de cada jugador
    //---- jugadores actuales
    //---- maximo numero de fichas por jugador
    public static ClassicDominoState ClassicDoubleSixGameStarter(Dictionary<string,object> Params)
    {
        if((IDominoState<int>)Params["State"] != null)
            return (ClassicDominoState)Params["State"];
        int pos = 0;
        foreach(ClassicDominoPiece piece in ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]])
        {
            if(piece.Left == piece.Right && piece.Right == (int)Params["MaxNumberOfPieces"] - 1)
            {
                ClassicDominoState state = new ClassicDominoState(piece,((IDominoPlayer<int>[])Params["CurrentPlayers"])[0].Name);
                ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]].RemoveAt(pos);
                return state;
            }
            pos++;
        }
        return null;
    }
    //Parametros
    //---- estado inicial del juego
    //---- fichas de cada jugador
    //---- jugadores actuales
    //---- maximo numero de fichas por jugador
    //---- jugadores del juego
    public static ClassicDominoState ClassicDoubleNineStarter(Dictionary<string,object> Params)
    {
        if((IDominoState<int>)Params["State"] != null)
            return (ClassicDominoState)Params["State"];

        DominoMovement<int> movement = ((IDominoPlayer<int>[])Params["Players"])[(int)Params["Player"]].Play(Params);
        for(int i = 0; i < ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]].Count; i++)
        {
            if(((ClassicDominoPiece)movement.Pieces[0]).Equals(((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]][i]))
            {
                ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]].RemoveAt(i);
                break;
            }   
        }
        return new ClassicDominoState((ClassicDominoPiece)movement.Pieces[0],movement.Player);
    }
}