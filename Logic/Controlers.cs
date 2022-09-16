namespace Logic;
public static class Controlers
{
    public static bool PairSumControler(int a, int b)
    {
        return (a + b) % 2 == 0;
    }
    public static bool NoPairSumControler(int a, int b)
    {
        return (a + b) % 2 != 0;
    }
    public static bool NormalControler(int a, int b)
    {
        return a == b;
    }
    public static bool SixMultipleControler(int a, int b)
    {
        return (a + b) % 6 == 0;
    }
}