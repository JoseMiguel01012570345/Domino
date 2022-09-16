namespace Logic;
public static class Utils
{
    public static bool IsDouble(ClassicDominoPiece Piece)
    {
        return Piece.Left == Piece.Right;
    }
    public static bool ContainsDouble(int value,IDominoPiece<int>[] Pieces)
    {
        foreach(var piece in Pieces)
        {
            if(IsDouble((ClassicDominoPiece)piece) && ((ClassicDominoPiece)piece).Left == value)
                return true;
        }
        return false;
    }
}