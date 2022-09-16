namespace Logic;
//caracteristicas de toda ficha de domino
public interface IDominoPiece<T>
{
    public T[] Values{get;}
    //todas tienen extremos por donde se pueden enlazar otras fichas
    public T[] Tops{get;}
    // necesitamos saber si entre los extremos de la ficha se halla un valor dado
    public bool Contains(T Value,Func<T,T,bool> Controler);
    //para guardar quien la coloco
    public void MatchHead(T value);
    public static string? Description{get;}
}
