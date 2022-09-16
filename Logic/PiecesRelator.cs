using System.Collections.Generic;
namespace Logic;
//simple porque se asume que no existen fichas repetidas
public class SimplePiecesRelator<T> : IPiecesRelator<T>
{
    Dictionary<string,List<string>> PiecesByPlayer;
    Dictionary<string,IDominoPiece<T>> Fathers;
    Dictionary<string,List<IDominoPiece<T>>> PiecesLinkedByPieces;
    public SimplePiecesRelator(string Player,params string[] initials)
    {
        PiecesByPlayer = new Dictionary<string, List<string>>();
        Fathers = new Dictionary<string, IDominoPiece<T>>();
        PiecesLinkedByPieces = new Dictionary<string, List<IDominoPiece<T>>>();
        foreach(var piece in initials)
        {
            PiecesLinkedByPieces[piece] = new List<IDominoPiece<T>>();
            Fathers[piece] = null;
        }
        PiecesByPlayer[Player] = new List<string>();
        foreach(var piece in initials)
            PiecesByPlayer[Player].Add(piece);
    }
    public void LinkPieceTo(IDominoPiece<T> piece, IDominoPiece<T> other, string Owner, bool father = false)
    {
        if(father)
        {
            if(!PiecesByPlayer.Keys.Contains(Owner))
                PiecesByPlayer[Owner] = new List<string>();
            Fathers[other.ToString()] = piece;
            PiecesByPlayer[Owner].Add(other.ToString());
            PiecesLinkedByPieces[piece.ToString()].Add(other);
            return;
        }
        if(!PiecesByPlayer.Keys.Contains(Owner))
            PiecesByPlayer[Owner] = new List<string>();
        PiecesByPlayer[Owner].Add(other.ToString());
        if(!PiecesLinkedByPieces.Keys.Contains(piece.ToString()))
            PiecesLinkedByPieces[piece.ToString()] = new List<IDominoPiece<T>>();
        PiecesLinkedByPieces[piece.ToString()].Add(other);
    }
    public IDominoPiece<T>[] PiecesLinkedOf(string Piece)
    {
        try
        {
            return PiecesLinkedByPieces[Piece].ToArray();
        }
        catch(Exception e)
        {
            throw new ArgumentException("La ficha no ha sido colocada");
        }
    }
    public IDominoPiece<T> FatherOf(string Piece)
    {
        if(Fathers.Keys.Contains(Piece))
            return Fathers[Piece];
        throw new ArgumentException("la ficha no ha sido colocada");
    }
    public string OwnerOf(string Piece)
    {
        foreach(var player in PiecesByPlayer.Keys)
            if(PiecesByPlayer[player].Contains(Piece))
                return player;
        throw new ArgumentException("la ficha no ha sido colocada");
    }
}