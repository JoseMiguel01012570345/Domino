namespace Logic;
//clase que define una jugada de domino
public class DominoMovement<T>
{
    //fichas que jugo el jugador
    public IDominoPiece<T>[] Pieces{get;private set;}
    //extremos por donde las jugo
    public T[] Tops{get;private set;}
    //jugador que realizo la jugada
    public string Player{get;private set;}
    public DominoMovement(IDominoPiece<T>[] Pieces,T[] Tops,string Player)
    {
        this.Pieces=Pieces;
        this.Player=Player;
        this.Tops=Tops;
    }
   
    public override string ToString()
    {
        if(Pieces==null)
            return "El jugador "+Player+" se ha pasado";
        string result="";
        foreach(var piece in Pieces)
            result+=piece.ToString()+" ";
        return "El jugador "+Player+" ha jugado "+result;
    }
}