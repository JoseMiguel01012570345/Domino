namespace Visual;
public interface IPaintable
{
    public int Width { get; }
    public int CenterX { get; set; }
    public int CenterY { get; set; }
    public void Paint(Graphics graphics);
    public void SetRotation(int degrees);
}