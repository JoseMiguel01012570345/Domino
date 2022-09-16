using Logic;

namespace Visual;
public class VisualClassicDominoState:IVisualDominoState<int>
{
    ClassicDominoState State {get; set; }
    List<VisualClassicDominoPiece> Pieces {get; set; }
    Graphics graphics {get; set; }
    VisualClassicDominoPiece Left {get; set; }
    VisualClassicDominoPiece Right {get; set; }
    int Width {get; set; }
    int LeftTop {get; set; }
    int RightTop {get; set; }
    int PiecesInRight {get; set; }
    int PiecesInLeft {get; set; }
    public VisualClassicDominoState(ClassicDominoState State, Graphics graphics, int Width, int CenterX, int CenterY)
    {
        this.Width = Width;
        this.State = State;
        this.graphics = graphics;
        Pieces = new List<VisualClassicDominoPiece>();
        VisualClassicDominoPiece initial = ((ClassicDominoPiece)State.Initials[0]).GetVisual(Width,CenterX,CenterY);
        LeftTop = ((ClassicDominoPiece)State.Initials[0]).Left;
        RightTop = ((ClassicDominoPiece)State.Initials[0]).Right;
        if(Utils.IsDouble((ClassicDominoPiece)State.Initials[0]))
            initial.SetRotation(90);
        else
            initial.SetRotation(0);
        Pieces.Add(initial);
        Left = initial;
        Right = initial;

    }
    public void Show()
    {
        foreach(var piece in Pieces)
            piece.Paint(graphics);
    }
    //revisar esto
    public void AddPiece(IDominoPiece<int> Piece, IDominoPiece<int> PieceTop,Func<int,int,bool> Controler)
    {
        if(State.PiecesPlayedTillNow.Length == 1)
        {
            if(Piece.Contains(((ClassicDominoPiece)PieceTop).Left,Controler))
                AddPieceToLeftBeforeTurn1((ClassicDominoPiece)Piece,Controler);
            else
                AddPieceToRightBeforeTurn1((ClassicDominoPiece)Piece,Controler);
        }
        else if(Left.Piece.Equals(PieceTop) && Piece.Contains(LeftTop,Controler))
        {
            if(PiecesInLeft < 10)
                AddPieceToLeftBeforeTurn1((ClassicDominoPiece)Piece, Controler);
            else if(PiecesInLeft == 10)
                AddPieceToLeftInTurn1((ClassicDominoPiece)Piece, Controler);
            else if(PiecesInLeft < 12)
                AddPieceToLeftAfterTurn1((ClassicDominoPiece)Piece, Controler);
            else if(PiecesInLeft == 12)
                AddPieceToLeftInTurn2((ClassicDominoPiece)Piece, Controler);
            else
                AddPieceToLeftAfterTurn2((ClassicDominoPiece)Piece, Controler);
        }
        else
        {
            if(PiecesInRight < 10)
                AddPieceToRightBeforeTurn1((ClassicDominoPiece)Piece, Controler);
            else if(PiecesInRight == 10)
                AddPieceToRightInTurn1((ClassicDominoPiece)Piece, Controler);
            else if(PiecesInRight < 12)
                AddPieceToRightAfterTurn1((ClassicDominoPiece)Piece, Controler);
            else if(PiecesInRight == 12)
                AddPieceToRightInTurn2((ClassicDominoPiece)Piece, Controler);
            else
                AddPieceToRightAfterTurn2((ClassicDominoPiece)Piece, Controler);
        }
    }
    void AddPieceToLeftInTurn1(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInLeft++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Left.CenterX,Left.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterX -= (3 * Width) / 2;
            piece.SetRotation(90);
        }
        else if(Utils.IsDouble(Left.Piece))
        {
            piece.CenterY -= 2 * Width;
            if(!Controler(Piece.Left,LeftTop))
            {
                piece.SetRotation(90);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(270);
                LeftTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterY -= (3 * Width) / 2;
            piece.CenterX -= Width / 2;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(90);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(270);
                LeftTop = Piece.Right;
            }
        }
        Left = piece;
        Pieces.Add(piece);
    }
    void AddPieceToRightInTurn1(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInRight++;
        VisualClassicDominoPiece piece =Piece.GetVisual(Width,Right.CenterX,Right.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterX += (3 * Width) / 2;
            piece.SetRotation(90);
        }
        else if(Utils.IsDouble(Right.Piece))
        {
            piece.CenterY += 2 * Width;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(270);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(90);
                RightTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX += Width / 2;
            piece.CenterY += (3 * Width) / 2;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(270);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(90);
                RightTop = Piece.Right;
            }
        }
        Right = piece;
        Pieces.Add(piece);
    }
    void AddPieceToLeftAfterTurn1(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInLeft++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Left.CenterX,Left.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterY -= (3 * Width) / 2;
            piece.SetRotation(0);
        }
        else if(Utils.IsDouble(Left.Piece))
        {
            piece.CenterY -= 2 * Width;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(90);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(270);
                LeftTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterY -= 2 * Width;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(90);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(270);
                LeftTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Left = piece;
    }
    void AddPieceToRightAfterTurn1(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInRight++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Right.CenterX,Right.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterY += (3 * Width) / 2;
            piece.SetRotation(0);
        }
        else if(Utils.IsDouble(Right.Piece))
        {
            piece.CenterY += 2 * Width;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(270);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(90);
                RightTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterY += 2 * Width;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(270);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(90);
                RightTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Right = piece;
    }
    void AddPieceToLeftInTurn2(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInLeft++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Left.CenterX,Left.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterY -= (3 * Width) / 2;
            piece.SetRotation(0);
        }
        else if(Utils.IsDouble(Left.Piece))
        {
            piece.CenterX += 2 * Width;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(180);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(0);
                LeftTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX += (3 * Width) / 2;
            piece.CenterY -= Width / 2;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(180);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(0);
                LeftTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Left = piece;
    }
    void AddPieceToRightInTurn2(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInRight++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Right.CenterX,Right.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterY += (3 * Width) / 2;
            piece.SetRotation(0);
        }
        else if(Utils.IsDouble(Right.Piece))
        {
            piece.CenterX -= 2 * Width;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(0);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(180);
                RightTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX -= (3 * Width) / 2;
            piece.CenterY += Width / 2;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(0);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(180);
                RightTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Right = piece;
    }
    void AddPieceToLeftAfterTurn2(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Left.CenterX,Left.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterX += (3 * Width) / 2;
            piece.SetRotation(90);
        }
        else if(Utils.IsDouble(Left.Piece))
        {
            if(Left.Rotation == RotateFlipType.RotateNoneFlipNone || Left.Rotation == RotateFlipType.Rotate180FlipNone)
                piece.CenterX += 2 * Width;
            else
                piece.CenterX += (3 * Width) / 2;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(180);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(0);
                LeftTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX += 2 * Width;
            if(!Controler(Piece.Left, LeftTop))
            {
                piece.SetRotation(180);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(0);
                LeftTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Left = piece;
    }
    void AddPieceToRightAfterTurn2(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Right.CenterX,Right.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterX -= (3 * Width) / 2;
            piece.SetRotation(90);
        }
        else if(Utils.IsDouble(Right.Piece))
        {
            if(Right.Rotation == RotateFlipType.RotateNoneFlipNone || Right.Rotation == RotateFlipType.Rotate180FlipNone)
                piece.CenterX -= 2 * Width;
            else
                piece.CenterX -= (3 * Width) / 2;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(0);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(180);
                RightTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX -= 2 * Width;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(0);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(180);
                RightTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Right = piece;
    }
    void AddPieceToLeftBeforeTurn1(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInLeft++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Left.CenterX,Left.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.CenterX -= (3 * Width) / 2;
            piece.SetRotation(90);
        }
        else if(Utils.IsDouble(Left.Piece))
        {
            piece.CenterX -= (3 * Width) / 2;
            if(!Controler(LeftTop, Piece.Left))
            {
                piece.SetRotation(0);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(180);
                LeftTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX -= 2 * Width;
            if(!Controler(LeftTop,Piece.Left))
            {
                piece.SetRotation(0);
                LeftTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(180);
                LeftTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Left = piece;
    }
    void AddPieceToRightBeforeTurn1(ClassicDominoPiece Piece,Func<int,int,bool> Controler)
    {
        PiecesInRight++;
        VisualClassicDominoPiece piece = Piece.GetVisual(Width,Right.CenterX,Right.CenterY);
        if(Utils.IsDouble(Piece))
        {
            piece.SetRotation(90);
            piece.CenterX += (3 * Width) / 2;
        }
        else if(Utils.IsDouble(Right.Piece))
        {
            piece.CenterX += (3 * Width) / 2;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(180);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(0);
                RightTop = Piece.Right;
            }
        }
        else
        {
            piece.CenterX += 2 * Width;
            if(!Controler(Piece.Left, RightTop))
            {
                piece.SetRotation(180);
                RightTop = Piece.Left;
            }
            else
            {
                piece.SetRotation(0);
                RightTop = Piece.Right;
            }
        }
        Pieces.Add(piece);
        Right = piece;
    }
}