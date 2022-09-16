namespace Logic;
public interface IDominoSorterPieces<T>:IDomino<T>,ISorterPieces<T>
{
    //para saber si ya se han repartido las fichas
    public bool Sorted { get; }
}

public interface IDominoPiecesSorter<T>:IDomino<T>,IPiecesSorter<T>
{
    
}