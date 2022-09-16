namespace Logic;
//caracteristicas de todo jugador de domino
public interface IDominoPlayer<T>:IPlayer<T>,IPiecesSelecter<T>
{
    //debe tener un nombre
    public string Name{ get; }
}