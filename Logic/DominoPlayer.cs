using System.Collections.Generic;
namespace Logic;
public class DominoPlayer<T>:IDominoPlayer<T>
{
    IStrategy<T> Strategy { get; set; }
    ISelecter<T> Selecter{ get; set; }
    public Random random { get; protected set; }
    public bool PlayMode { get; protected set; }
    public bool SelectMode { get; protected set; }
    public string Name{ get; protected set; }
    public DominoPlayer(string Name)
    {
        random = new Random();
        this.Name = Name;
        PlayMode = false;
        SelectMode = false;
    }
    public virtual void SetPlayMode(IStrategy<T> Strategy)
    {
        this.Strategy = Strategy;
        if(Strategy != null)
            PlayMode = true;
        else
            PlayMode = false;
    }
    public virtual void SetSelecter(ISelecter<T> Selecter)
    {
        this.Selecter = Selecter;
        if(Selecter != null)
            SelectMode = true;
        else
            SelectMode = false;
    }
    public virtual DominoMovement<T> Play(Dictionary<string,object> Params)
    {
        if(!PlayMode)
        {
            IDominoPiece<T>[] pieces = ((Func<string[],IDominoPiece<T>[]>)Params["Holder"])(new[] { this.Name });
            foreach(var piece in pieces)
                foreach(var top in ((IDominoState<T>)Params["State"]).Tops)
                    if(piece.Contains(top,((Func<T,T,bool>)Params["Controler"])))
                        return new DominoMovement<T>(new[] { piece }, new[] { top }, this.Name);
            return new DominoMovement<T>(null,null,this.Name);
        }
        return Strategy.ExecuteStrategy(Params,this.Name);
    }
    public virtual IDominoPiece<T> Select(Dictionary<string, object> Params)
    {
        if(!SelectMode)
        {
            IDominoPiece<T>[] pieces = ((Func<string[],IDominoPiece<T>[]>)Params["Holder"]).Invoke(new[] { this.Name });
            return pieces[random.Next(pieces.Length)];
        }
        return Selecter.SelectPiece(Params,this.Name);
    }
}