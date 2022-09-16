using System.Collections.Generic;
namespace Logic;
public class RandomSelecter:ISelecter<int>
{
    Random random;
    public RandomSelecter()
    {
        random = new Random();
    }
    public IDominoPiece<int> SelectPiece(Dictionary<string,object> Params,string Player)
    {
        IDominoPlayer<int> p = null;
        foreach(var player in(IDominoPlayer<int>[])Params["Players"])
            if(player.Name == Player)
                p = player;
        IDominoPiece<int>[] Pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
        return Pieces[random.Next(Pieces.Length)];   
    }
}   
public class ThrowFatSelecter:ISelecter<int>
{
    public IDominoPiece<int> SelectPiece(Dictionary<string,object> Params, string Player)
    {
        IDominoPiece<int>[] Pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
        return Pieces[Pieces.Length - 1];
    }
}