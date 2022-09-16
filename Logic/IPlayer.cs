using System.Collections.Generic;
namespace Logic;
public interface IPLayModeSelector<T>
{
    //para saber si existe un modo de jugar
    public bool PlayMode { get; }
    //debe poder seleccionar el modo de jugar
    public void SetPlayMode(IStrategy<T> Strategy);
}
public interface IPlayer<T>:IPLayModeSelector<T>
{
    //debe poder jugar
    public DominoMovement<T> Play(Dictionary<string,object> Params);
}