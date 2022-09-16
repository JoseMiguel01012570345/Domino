namespace Logic;
public static class Comparers
{
    public static int ClassicCompare(IDominoPiece<int> piece1, IDominoPiece<int> piece2)
    {
        int piece1Sum = 0;
        int piece2Sum = 0;
        foreach(var value in piece1.Values)
            piece1Sum += value;
        foreach(var value in piece2.Values)
            piece2Sum += value;
        return Math.Sign(piece1Sum - piece2Sum);
    }
}