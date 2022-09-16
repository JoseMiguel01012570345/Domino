using System.Collections.Generic;
namespace Logic;
public interface IPiecesRelator<T>
{
    public IDominoPiece<T> FatherOf(string Piece);
    public IDominoPiece<T>[] PiecesLinkedOf(string Piece);
    public string OwnerOf(string Piece);
    public void LinkPieceTo(IDominoPiece<T> Piece,IDominoPiece<T> other, string Owner, bool father = false);
}