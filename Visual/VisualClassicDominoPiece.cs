using Logic;

namespace Visual;
public class VisualClassicDominoPiece:IPaintable
{
    public ClassicDominoPiece Piece { get; private set;}
    public int Width { get; private set;}
    public int CenterX { get; set;}
    public int CenterY { get; set;}
    public RotateFlipType Rotation { get; private set;}
    public RotateFlipType LeftRotationImage { get; private set;}
    public RotateFlipType RightRotationImage { get; private set;}
    public VisualClassicDominoPiece(ClassicDominoPiece Piece, int Width, int CenterX, int CenterY)
    {
        this.Piece = Piece;
        this.Width = Width;
        this.CenterX = CenterX;
        this.CenterY = CenterY;
        Rotation = RotateFlipType.RotateNoneFlipNone;
        LeftRotationImage = RotateFlipType.RotateNoneFlipNone;
        RightRotationImage = RotateFlipType.Rotate180FlipNone;
    }
    string LeftImageLocation
    {
        get
        {
            return @"D:\Proyectos_no_borrar\Yonatan_Proyecto_No_Borrar_New\Visual\PiecesImages\Pieces\" + Piece.Left + ".png";
        }
    }
    string RightImageLocation
    {
        get
        {
            return @"D:\Proyectos_no_borrar\Yonatan_Proyecto_No_Borrar_New\Visual\PiecesImages\Pieces\" + Piece.Right + ".png";
        }
    }
    Bitmap LeftImage
    {
        get
        {
            Bitmap Image = new Bitmap(new Bitmap(LeftImageLocation),Width,Width);
            Image.RotateFlip(LeftRotationImage);
            return Image;
        }
    }
    Bitmap RightImage
    {
        get
        {
            Bitmap Image = new Bitmap(new Bitmap(RightImageLocation),Width,Width);
            Image.RotateFlip(RightRotationImage);
            return Image;
        }
    }
    public int LeftX
    {
        get
        {
            if(Rotation == RotateFlipType.RotateNoneFlipNone)
                return CenterX - Width;
            if(Rotation == RotateFlipType.Rotate90FlipNone)
                return CenterX - Width / 2;
            if(Rotation == RotateFlipType.Rotate180FlipNone)
                return CenterX;
            return CenterX - Width / 2;
        }
    }
    public int LeftY
    {
        get
        {
            if(Rotation == RotateFlipType.RotateNoneFlipNone)
                return CenterY - Width / 2;
            if(Rotation == RotateFlipType.Rotate90FlipNone)
                return CenterY - Width;
            if(Rotation == RotateFlipType.Rotate180FlipNone)
                return CenterY - Width / 2;
            return CenterY;
        }
    }
    public int RightX
    {
        get
        {
            if(Rotation == RotateFlipType.RotateNoneFlipNone)
                return CenterX + 1;
            if(Rotation == RotateFlipType.Rotate90FlipNone)
                return CenterX - Width / 2 - 1;
            if(Rotation == RotateFlipType.Rotate180FlipNone)
                return CenterX - Width - 1;
            return CenterX - Width / 2 + 1;
        }
    }
    public int RightY
    {
        get
        {
            if(Rotation == RotateFlipType.RotateNoneFlipNone)
                return CenterY - Width / 2 + 1;
            if(Rotation == RotateFlipType.Rotate90FlipNone)
                return CenterY + 1;
            if(Rotation == RotateFlipType.Rotate180FlipNone)
                return CenterY - Width / 2 - 1;
            return CenterY - Width - 1;
        }
    }
    void SetRotation90()
    {
        Rotation = RotateFlipType.Rotate90FlipNone;
        LeftRotationImage = RotateFlipType.Rotate90FlipNone;
        RightRotationImage = RotateFlipType.Rotate270FlipNone;
    }
    void SetRotation180()
    {
        Rotation = RotateFlipType.Rotate180FlipNone;
        LeftRotationImage = RotateFlipType.Rotate180FlipNone;
        RightRotationImage = RotateFlipType.RotateNoneFlipNone;
    }
    void SetRotation270()
    {
        Rotation = RotateFlipType.Rotate270FlipNone;
        LeftRotationImage = RotateFlipType.Rotate270FlipNone;
        RightRotationImage = RotateFlipType.Rotate90FlipNone;
    }
    void SetRotation0()
    {
        Rotation = RotateFlipType.RotateNoneFlipNone;
        LeftRotationImage = RotateFlipType.RotateNoneFlipNone;
        RightRotationImage = RotateFlipType.Rotate180FlipNone;
    }
    public void Paint(Graphics graphics)
    {
        graphics.DrawImage(LeftImage,LeftX,LeftY);
        graphics.DrawImage(RightImage,RightX,RightY);
    }
    public void SetRotation(int degrees)
    {
        switch(degrees)
        {
            case 0:
                SetRotation0();
                break;
            case 90:
                SetRotation90();
                break;
            case 180:
                SetRotation180();
                break;
            case 270:
                SetRotation270();
                break;
            default:
                SetRotation0();
                break;
        }
    }
}