using System.Collections.Generic;
namespace Logic;
public static class Updaters
{
    //Parametros
    //---- saber si el juego se inicializo
    //---- saber si el juego termino
    //---- el estado del juego
    //---- el metod inicializador
    //---- el controlador de compatibilidad de fichas del juego
    //---- los jugadores actuales
    //---- las fichas de cada jugador
    public static DominoMovement<int> ClassicUpdate(Dictionary<string,object> Params)
    {
        if(!((bool)Params["Started"]))
            throw new InvalidOperationException("EL juego no se ha inicializado");
        if((bool)Params["GameOver"])
        {
            return null;
        }
        if((IDominoState<int>)Params["State"] == null)
        {
            ((Action)Params["FirstMovement"]).Invoke();
            return new DominoMovement<int>(((IDominoState<int>)Params["State"]).Initials,new[] { 0 },((IDominoPlayer<int>[])Params["CurrentPlayers"])[0].Name);
        }
        DominoMovement<int> movement = ((IDominoPlayer<int>[])Params["CurrentPlayers"])[0].Play(Params);
        if(movement.Pieces != null)
        {
            ClassicDominoPiece piecePlayed = (ClassicDominoPiece)movement.Pieces[0];
            try
            {
                ((IDominoState<int>)Params["State"]).AddPiece(movement.Pieces[0],movement.Tops[0],((Func<int,int,bool>)Params["Controler"]),movement.Player);
                for(int i = 0; i < ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]].Count; i++)
                {
                    if(((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]][i].Equals(piecePlayed))
                    {
                        ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[((IDominoPlayer<int>[])Params["CurrentPlayers"])[0]].RemoveAt(i);
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                throw new InvalidOperationException("Ha ocurrido una violacion de las reglas");
            }
        }
        return movement;
    }
}