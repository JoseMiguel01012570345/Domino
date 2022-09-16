using System.Collections.Generic;
namespace Logic;
public interface IPiecesSorter<T>:IComparer<IDominoPiece<T>>,ISorterSelector<T>,IComparerSelector<IDominoPiece<T>>
{
    //debe poder ordenar fichas
    public IDominoPiece<T>[] Sort(IDominoPiece<T>[] Pieces);
}
public interface ISorterSelector<T>
{
    //permite elegir de que forma se ordenaran las fichas
    public void SetSorter(Func<Dictionary<string,object>,IDominoPiece<T>[],IDominoPiece<T>[]> Sorter);
}