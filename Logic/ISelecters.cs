namespace Logic;
public interface ISelecter<T>
{
    public IDominoPiece<T> SelectPiece(Dictionary<string, object> Params,string Player);
}