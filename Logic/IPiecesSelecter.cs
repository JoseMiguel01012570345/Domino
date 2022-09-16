using System.Collections.Generic;
namespace Logic;
public interface ISelectPiecesSelector<T>
{
    //para saber si existe un modo de elegir fichas
    public bool SelectMode { get; }
    //debe poder elegir de que forma va a elegir las fichas
    public void SetSelecter(ISelecter<T> SelectMode);
}
public interface IPiecesSelecter<T>:ISelectPiecesSelector<T>
{
    //para pder seleccionar fichas
    public IDominoPiece<T> Select(Dictionary<string,object> Params);
}