namespace Logic;
//caracteristicas de una mesa de domino
public interface IDominoState<T>
{
    //cantidad de fichas que se han colocado
    public int Count{get;}
    //fichas con las que se inicio el juego
    public IDominoPiece<T>[] Initials{get;}
    public IPiecesRelator<T> Relator { get; }
    //fichas que se jugaron en el ultimo turno
    public IDominoPiece<T>[] PiecesPlayed{get;}
    //fichas jugadas hasta el momento
    public IDominoPiece<T>[] PiecesPlayedTillNow{get;}
    //extremos por donde se pueden enlazar nuevas fichas al juego
    public T[] Tops{get;}
    //fichas extremo por donde se pueden enlazar nuevas fichas
    public IDominoPiece<T>[] PiecesTops{get;}
    //debe poder agregar una ficha nueva al juego
    public void AddPiece(IDominoPiece<T> piece, T Top, Func<T,T,bool> Controler,string Player);
    //debe poder cambiar el jugador que esta jugando
    public void ChangePlayer();
}