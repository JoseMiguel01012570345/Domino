using System.Collections.Generic;
namespace Logic;
//clase base de todos los dominos
public abstract class Domino<T>:IDomino<T>,IGameModesSelector<T>
{
    protected Func<Dictionary<string,object>,IDominoState<T>>? GameStarter { get; set; }
    protected Func<Dictionary<string,object>,bool>? GameOverer { get; set; }
    protected Func<Dictionary<string,object>,int>? Judge { get; set; }
    protected Func<Dictionary<string,object>,DominoMovement<T>>? Updater { get; set; }
    protected Func<Dictionary<string,object>,int>? Starter { get; set; }
    protected Func<Dictionary<string,object>,IDominoPlayer<T>[]>? WinnerComputer { get; set; }
    protected IDominoPlayer<T>[]? winners { get; set; }
    protected IDominoPlayer<T>? currentPlayer { get; set; }
    protected IDominoPlayer<T>[]? players { get; set; }
    protected IDominoState<T>? state { get; set; }
    protected DominoMovement<T>? lastMovement { get; set; }
    public List<IDominoPiece<T>> Pieces { get; protected set; }
    public Func<T,T,bool> Controler { get; protected set; }
    public Dictionary<IDominoPlayer<T>,int> JumpsByPlayer { get; protected set; }
    protected Dictionary<string,object> Params { get; set; }
    protected bool started { get; set; }
    public bool[]? PlayersPlaying { get; protected set; }
    public int MaxNumberOfPieces { get; set; }
    protected bool gameOver { get; set; }
    public int player { get; protected set; }
    public IDominoPlayer<T>[] Winners
    {
        get
        {
            if (gameOver)
            {
                IDominoPlayer<T>[]? winners = WinnerComputer(Params);
                IDominoPlayer<T>[] result = new IDominoPlayer<T>[winners.Length];
                Array.Copy(winners, result, result.Length);
                return result;
            }
            else throw new Exception("El juego no se ha acabado");
        }
    }
    public IDominoPlayer<T>[] CurrentPlayers
    {
        get{ return GetCurrentPlayers(); }
    }
    public IDominoPlayer<T>[] Players
    {
        get{ return GetPlayers(); }
    }
    public IDominoState<T> State
    {
        get{ return state; }
    }
    public DominoMovement<T> LastMovement
    {
        get{ return new DominoMovement<T>(lastMovement.Pieces,lastMovement.Tops,lastMovement.Player); }
    }
    public bool Started
    {
        get{ return started; }
    }
    protected virtual IDominoPlayer<T>[] GetCurrentPlayers()
    {
        return new[] { currentPlayer };
    }
    protected virtual IDominoPlayer<T>[] GetPlayers()
    {
        IDominoPlayer<T>[] result = new IDominoPlayer<T>[players.Length];
        Array.Copy(players,result,result.Length);
        return result;
    }
    public virtual void SetPlayers(params IDominoPlayer<T>[] players)
    {
        this.players = players;
        PlayersPlaying = new bool[players.Length];
        Params["Players"] = players;
    }
    public virtual void SetGameStarter(Func<Dictionary<string,object>,IDominoState<T>> GameStarter)
    {
        this.GameStarter = GameStarter;
    }
    public virtual void SetGameOver(Func<Dictionary<string,object>,bool> GameOverer)
    {
        this.GameOverer = GameOverer;
    }
    public virtual void SetJudger(Func<Dictionary<string,object>,int> Judge)
    {
        this.Judge = Judge;
    }
    public virtual void SetUpdater(Func<Dictionary<string,object>,DominoMovement<T>> Updater)
    {
        this.Updater = Updater;
    }
    public virtual void SetStarter(Func<Dictionary<string,object>, int> Starter)
    {
        this.Starter = Starter;
    }
    public virtual void SetWinnerComputer(Func<Dictionary<string,object>,IDominoPlayer<T>[]> WinnerComputer)
    {
        this.WinnerComputer = WinnerComputer;
    }
    public virtual void SetControler(Func<T,T,bool> Controler)
    {
        this.Controler = Controler;
    }
    public abstract void FirstMovement();
    public abstract bool GameOver();
    public abstract void NextPlayer();
    public abstract void Update();
    public abstract void Start();
    public abstract IDominoPiece<T>[] ShowPieces(params string[] Players);  
}