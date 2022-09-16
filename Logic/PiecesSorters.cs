namespace Logic;
public static class PiecesSorters
{
    static Random random = new Random();
    public static int TotalPieces = 3;
    //Parametros:
    //---- los jugadores
    //---- las fichas del juego
    //---- las fichas por jugador
    public static void ClassicStaticSortPieces(Dictionary<string,object> Params)
    {
        foreach(var player in ((IDominoPlayer<int>[])Params["Players"]))
        {
            ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[player] = new List<IDominoPiece<int>>();
            for(int i = 0; i < TotalPieces; i++)
            {
                int pos = random.Next(((List<IDominoPiece<int>>)Params["Pieces"]).Count);
                ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"])[player].Add(((List<IDominoPiece<int>>)Params["Pieces"])[pos]);
                ((List<IDominoPiece<int>>)Params["Pieces"]).RemoveAt(pos);
            }
        }
    }
}