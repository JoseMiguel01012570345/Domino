namespace Logic;
//ficha clasica de domino de dos lados con puntos en ellos
public class ClassicDominoPiece : IDominoPiece<int>
{
    //fichas con las que se encuentra enlazada
    bool[] piecesLinked { get; set; }
    //valores de los extremos de la ficha
    int[] values { get; set; }
    static string description = "Fichas clasicas del domino de dos lados con números en los extremos formados por puntos.";
    public ClassicDominoPiece(int Left, int Right)
    {
        values = new[] { Left, Right };
        piecesLinked = new bool[2];
    }
    public static string Description
    {
        get{ return description; }
    }
    //devuelve los valores de los extremos de la ficha
    public int[] Values
    {
        get{ return new[] { Left, Right}; }
    }
    public void MatchHead(int value)
    {
        for(int i = 0; i < values.Length; i++)
            if(values[i] == value && !piecesLinked[i])
            {
                piecesLinked[i] = true; 
                break;
            }
    }
    //devuelve los valores de los extremos libres de la ficha por donde se puede enlazar otra
    public int[] Tops
    {
        get
        {
            if(!piecesLinked[0]  && !piecesLinked[1] )
                return new[] { Values[0], Values[1] };
            if(!piecesLinked[0])
                return new[] { Values[0] };
            if(!piecesLinked[1])
                return new[] { Values[1] };
            return new int[0];
        }
    }
    //valor izquierdo de la ficha
    public int Left
    {
        get { return values[0]; }
    }
    // valor derecho de la ficha
    public int Right
    {
        get { return values[1]; }
    }
    //ficha enlazada por la izquierda a esta ficha
    public ClassicDominoPiece LeftPiece(IDominoPiece<int>[] Pieces)
    {
        try
        {
            return (ClassicDominoPiece)Pieces[0];
        }
        catch(Exception e)
        {
            return null;
        }
    }
    //ficha enlazad por la derecha a esta ficha
    public ClassicDominoPiece RightPiece(IDominoPiece<int>[] Pieces)
    {
        try
        {
            return (ClassicDominoPiece)Pieces[1];
        }
        catch(Exception e)
        {
            return null;
        }
    }
    //metodo que nos dice si un valor dado es compatible con alguno de los valores extremos de la ficha
    //segun un criterio dado
    public bool Contains(int Value, Func<int,int,bool> Controler)
    {
        return Controler(Left,Value) || Controler(Right, Value);
    }
    //metodo que especifica que jugador coloco esta ficha
    public override string ToString()
    {
        return "[" + Left + "|" + Right + "]";
    }
    
    public override bool Equals(object other)
    {
        return other == null ? false : this.ToString() == other.ToString();
    }
}