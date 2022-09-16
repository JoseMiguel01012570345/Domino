using Logic;

namespace Visual;
public static class WorkSpace
{
    public static ClassicDomino game = new ClassicDomino(5);
    public static void Start()
    {
        game.Start();        
    }
    public static void Update()
    {
        game.Update();
    }
    public static void Restart(int number_of_pieces)
    {
        game = new ClassicDomino(number_of_pieces);
    }
    public static void SetStrategy(string Strategy,IDominoPlayer<int> Player)
    {
        switch (Strategy)
        {
            case "Aleatorio":
                Player.SetPlayMode(new ClassicDominoRandomStrategy());
                break;
            case "Bota_Gorda":
                Player.SetPlayMode(new ClassicDominoThrowFatStrategy());
                break;
            case "Heuristico":
                Player.SetPlayMode(new ClassicDoubleSixAliHeuristicStrategy());
                break;
            case "Agachado":
                Player.SetPlayMode(new CrouchedDownClassicDominoPlayerStrategy());
                break;
            case "Simulador":
                Player.SetPlayMode(new SimulatorPlayerStrategy());
                break;
            default:
                throw new ArgumentException("No existe esa estrategia");
        }
    }
    public static void SetSelecter(string Selecter,IDominoPlayer<int> Player)
    {
        switch (Selecter)
        {
            case "Aleatorio":
                Player.SetSelecter(new RandomSelecter());
                break;
            case "Bota_Gorda":
                Player.SetSelecter(new ThrowFatSelecter());
                break;
            default:
                throw new ArgumentException("Modo no implementado");
        }
    }
}