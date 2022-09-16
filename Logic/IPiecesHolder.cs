namespace Logic;
public interface IPiecesHolder<T>
{
    //debe mostrar las fichas de un conjunto de jugadores
    public IDominoPiece<T>[] ShowPieces(params string[] Players);
}