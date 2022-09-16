namespace Logic;
public interface IComparerSelector<T>
{
    //debe poder elegir la forma de comparar las fichas
    public void SetComparer(Func<T,T,int> Comparer);
}