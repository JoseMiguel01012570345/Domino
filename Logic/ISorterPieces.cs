using System.Collections.Generic;
namespace Logic;
public interface ISorterPiecesSelector<T>
{
    //permite elegir de que forma se repartiran las fichas
    public void SetSorterPieces(Action<Dictionary<string,object>> SorterPieces);
}
//caracteristicas de todo juego que reparte fichas
public interface ISorterPieces<T>:ISorterPiecesSelector<T>
{
    //debe poder repartir fichas
    public void SortPieces();
    //debe llevar control de que fichas les tocaron a los jugadores
    public Dictionary<IDominoPlayer<T>,List<IDominoPiece<T>>> PiecesByPlayer{get;}
}