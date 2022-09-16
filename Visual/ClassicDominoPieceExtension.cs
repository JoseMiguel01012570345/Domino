using Logic;

namespace Visual;
public static class ClassicDominoPieceExtension
{
    public static VisualClassicDominoPiece GetVisual(this ClassicDominoPiece Piece, int Width, int CenterX, int CenterY)
    {
        return new VisualClassicDominoPiece(Piece, Width, CenterX, CenterY);
    }
}