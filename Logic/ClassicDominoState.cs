namespace Logic;
public class ClassicDominoState:IDominoState<int>
{
    public IPiecesRelator<int> Relator { get; private set; }
    ClassicDominoPiece initial { get; set; }
    List<ClassicDominoPiece> piecesPlayeds { get; set; }
    ClassicDominoPiece topLeftPiece {get; set; }
    ClassicDominoPiece topRightPiece { get; set; }
    public int TopLeft {get; private set; }
    public int TopRight { get; private set; }
    int count { get; set; }
    public ClassicDominoState(ClassicDominoPiece Initial,string Player)
    {
        initial = Initial;
        count = 1;
        piecesPlayeds = new List<ClassicDominoPiece>();
        piecesPlayeds.Add(initial);
        TopLeft = initial.Left;
        TopRight = initial.Right;
        topRightPiece = initial;
        topLeftPiece = initial;
        Relator = new SimplePiecesRelator<int>(Player,initial.ToString());
    }
    public int Count
    {
        get{ return count; }
    }
    public IDominoPiece<int>[] Initials
    {
        get
        {
            ClassicDominoPiece result = new ClassicDominoPiece(initial.Left,initial.Right);
            return new[] { result };
        }
    }
    public IDominoPiece<int>[] PiecesPlayed
    {
        get
        {
            ClassicDominoPiece[] result = new ClassicDominoPiece[piecesPlayeds.Count];
            Array.Copy(piecesPlayeds.ToArray(),result,result.Length);
            return result;
        }
    }
    public IDominoPiece<int>[] PiecesPlayedTillNow
    {
        get
        {
            ClassicDominoPiece[] result = new ClassicDominoPiece[count];
            int pos = 0;
            result[pos] = initial;
            pos++;
            ClassicDominoPiece leftPiece = initial.LeftPiece(Relator.PiecesLinkedOf(initial.ToString()));
            ClassicDominoPiece rightPiece = initial.RightPiece(Relator.PiecesLinkedOf(initial.ToString()));
            while(leftPiece != null && pos < count)
            {
                result[pos] = leftPiece;
                pos++;
                try
                {
                    foreach(var piece in Relator.PiecesLinkedOf(leftPiece.ToString()))
                        if(!result.Contains(piece))
                        {
                            leftPiece = (ClassicDominoPiece)piece;
                            break;
                        }
                }
                catch(Exception e)
                {
                    leftPiece = null;
                }
            }
            while(rightPiece != null && pos < count)
            {
                result[pos] = rightPiece;
                pos++;
                try
                {
                    foreach(var piece in Relator.PiecesLinkedOf(leftPiece.ToString()))
                        if(!result.Contains(piece))
                        {
                            rightPiece = (ClassicDominoPiece)piece;
                            break;
                        }
                }
                catch(Exception e)
                {
                    rightPiece = null;
                }
            }
            List<ClassicDominoPiece> Result = new List<ClassicDominoPiece>();
            foreach(var piece in result)
                if(piece != null)
                    Result.Add(piece);
            return Result.ToArray();
        }
    }
    public IDominoPiece<int>[] PiecesTops
    {
        get
        {
            ClassicDominoPiece left = new ClassicDominoPiece(topLeftPiece.Left,topLeftPiece.Right);
            //left.SetFather(topLeftPiece.Father);//eliminar esta linea
            ClassicDominoPiece right = new ClassicDominoPiece(topRightPiece.Left,topRightPiece.Right);
            //right.SetFather(topRightPiece.Father);//eliminar esta linea
            return new[] { left, right };
        }
    }
    public int[] Tops
    {
        get { return new[] { TopLeft, TopRight}; }
    }
    public void AddPiece(IDominoPiece<int> Piece, int Top, Func<int,int,bool> Controler,string Player)
    {
        //se calcula el extremo de la ficha que se quiere enlazar
        foreach(var top in Piece.Values)
            if(Controler(top,Top))
            {
                Top = top;
                break;
            }
        //se comprueba que las fichas sean enlazables
        if(!Controler(Top,TopLeft) && !Controler(Top,TopRight))
            throw new InvalidOperationException("Jugada invalida");
        Piece.MatchHead(Top);
        if(Controler(Top,TopLeft))
        {
            //Actualizamos las relaciones de las fichas
            Relator.LinkPieceTo(topLeftPiece,Piece,Player,true);
            //Lo volvemos a actualizar
            Relator.LinkPieceTo(Piece,topLeftPiece,Player);
            piecesPlayeds.Add((ClassicDominoPiece)Piece);
            topLeftPiece = (ClassicDominoPiece)Piece;
            TopLeft = Piece.Tops[0];
            count++;
        }
        else if(Controler(Top,TopRight))
        {
            Relator.LinkPieceTo(topRightPiece,Piece,Player,true);
            Relator.LinkPieceTo(Piece,topRightPiece,Player);
            piecesPlayeds.Add((ClassicDominoPiece)Piece);
            topRightPiece = (ClassicDominoPiece)Piece;
            TopRight = Piece.Tops[0];
            count++;
        }
    }
    public void ChangePlayer()
    {
        piecesPlayeds.Clear();
    }
}