using System.Collections.Generic;
namespace Logic;
public interface IStrategy<T>
{
    public DominoMovement<T> ExecuteStrategy(Dictionary<string,object> Params,string Player);
}