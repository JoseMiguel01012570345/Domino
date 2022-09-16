using System.Collections.Generic;
namespace Logic;
public static class WinnerComputers
{
    //Parametros
    //---- fichas de cada jugador
    //---- los jugadores que siguen jugando
    public static IDominoPlayer<int>[] WinnerComputerPieces(Dictionary<string,object> Params)//gana el que menos fichas tiene
    {
        int smallest = int.MaxValue;
        IDominoPlayer<int>[] winner = new IDominoPlayer<int>[1];
        int j = 0;
        foreach (var i in ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"]))
        {
            if (smallest > i.Value.Count && ((bool[])Params["PlayersPlaying"])![j] == false)
            {
                smallest = i.Value.Count;
                winner[0] = i.Key;
            }
            j++;
        }
        return winner;
    }
    //Parametros
    //---- jugadores del juego
    public static IDominoPlayer<int>[] WinnerComputer(Dictionary<string,object> Params)//gana quien menos turnos se ha pasado
    {
        IDominoPlayer<int>[] winner = new IDominoPlayer<int>[1];
        winner[0] = ((IDominoPlayer<int>[])Params["Players"])[Min<int>(Params)];

        return winner;
    }
    //Parametros
    //---- jugadores del juego
    //---- pases de cada jugador
    //---- jugadores que siguen jugando
    static int Min<T>(Dictionary<string,object> Params)
    {
        int smallest = int.MaxValue;
        for (int i = 0; i < ((IDominoPlayer<T>[])Params["Players"]).Length; i++)
        {
            if (smallest > ((Dictionary<IDominoPlayer<T>,int>)Params["JumpsByPlayer"])[((IDominoPlayer<T>[])Params["Players"])[i]] && ((bool[])Params["PlayersPlaying"])![i] == false) smallest = ((Dictionary<IDominoPlayer<T>,int>)Params["JumpsByPlayer"])[((IDominoPlayer<T>[])Params["Players"])[i]];
        }
        return smallest;
    }
    //Parametros
    //---- jugadores del juego
    //---- fichas de cada jugador
    public static IDominoPlayer<int>[] ClassicWinnerComputer(Dictionary<string,object> Params)
    {
        Dictionary<IDominoPlayer<int>,int> Points = new Dictionary<IDominoPlayer<int>, int>();
        foreach(var player in ((IDominoPlayer<int>[])Params["Players"]))
        {
            if(((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[player].Count == 0)
                return new[] { player };
            Points[player] = 0;
            foreach(var piece in ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[player])
            {
                foreach(var value in piece.Values)
                    Points[player] += value;
            }
        }
        IDominoPlayer<int> winner = ((IDominoPlayer<int>[])Params["Players"])[0];
        foreach(var player in ((IDominoPlayer<int>[])Params["Players"]))
        {
            if(Points[player] < Points[winner])
                winner = player;
        }
        List<IDominoPlayer<int>> winners = new List<IDominoPlayer<int>>();
        winners.Add(winner); 
        foreach(var player in ((IDominoPlayer<int>[])Params["Players"]))
            if(!player.Equals(winner) && Points[player] == Points[winner])
                winners.Add(player);
        return winners.ToArray();
    }
}