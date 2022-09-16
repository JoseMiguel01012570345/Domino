using Logic;

namespace Visual;
public interface IVisualDominoState<T>
{
    public void Show();
    public void AddPiece(IDominoPiece<T> Piece, IDominoPiece<T> Top,Func<T,T,bool> Controler);
}