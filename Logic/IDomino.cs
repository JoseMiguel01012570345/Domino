namespace Logic;
//caracteristicas de todo domino
public interface IDomino<T>:IGame<T>,IPiecesHolder<T>,IControlerSelector<T>
{
    //maximo numer de fichas al inicio por cada jugador
    public int MaxNumberOfPieces { get; }
    
    //debe poder llevar la cuenta de los pases por jugador
    public Dictionary<IDominoPlayer<T>,int> JumpsByPlayer { get; }
    //debe tener una manera de determinar los ganadores en caso de haber
    public IDominoPlayer<T>[] Winners{get;}
    //debe saber quienes son los jugadores que juegan en el momento
    public IDominoPlayer<T>[] CurrentPlayers{get;}
    //debe saber cuales son los jugadores que participan
    public IDominoPlayer<T>[] Players{get;}
    //debe tener un estado de  juego
    public IDominoState<T> State{get;}
    public Func<T,T,bool> Controler{get; }
    //debe poder establecer quienes jugaran el juego
    public void SetPlayers(IDominoPlayer<T>[] Players);
}