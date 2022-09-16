using System.Collections.Generic;
namespace Logic;
//Domino con fichas clasicas de dos lados con valores numericos
//
//En esta modalidad se reparte cierta cantidad de fichas por jugador y mientras mas puntos tenga la ficha
//mayor es su valor en el juego a la hora de contar
public class ClassicDomino:Domino<int>,IDominoPiecesSorter<int>,IDominoSorterPieces<int>
{
    //Parametros:
    //---- los jugadores
    //---- las fichas del juego
    //---- las fichas por jugador
    //---- maximo numero de fichas por jugador
    //---- el metodo que reparte las fichas
    protected Func<Dictionary<string,object>,IDominoPiece<int>[],IDominoPiece<int>[]> Sorter { get; set; }
    public Func<IDominoPiece<int>,IDominoPiece<int>,int> Comparer { get; protected set; }
    protected Action<Dictionary<string,object>> SorterPieces { get; set; }
    protected Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>> piecesByPlayer { get; set; }
    public bool Sorted { get; protected set; }
    //cantidad de pases consecutivos que se han dado
    public int Jumps { get; protected set; }
    public ClassicDomino(int MaxNumberOfPieces,params IDominoPlayer<int>[] players)
    {
        this.MaxNumberOfPieces = MaxNumberOfPieces;
        this.players = players;
        started = false;
        gameOver = false;
        player = 0;
        Jumps = 0;
        PlayersPlaying = new bool[players.Length];
        JumpsByPlayer = new Dictionary<IDominoPlayer<int>, int>();
        piecesByPlayer = new Dictionary<IDominoPlayer<int>, List<IDominoPiece<int>>>();
        Pieces = new List<IDominoPiece<int>>();
        for(int i = 0; i < MaxNumberOfPieces; i++)
        {
            for(int j = i; j < MaxNumberOfPieces; j++)
            {
                Pieces.Add(new ClassicDominoPiece(i, j));
            }
        }
        Params = new Dictionary<string, object>();
        Params["Players"] = players;
        Params["PiecesByPlayer"] = PiecesByPlayer;
        Params["Pieces"] = Pieces;
        Params["MaxNumberOfPieces"] = MaxNumberOfPieces;
        Params["State"] = state;
        Params["CurrentPlayers"] = new IDominoPlayer<int>[0];
        Params["Player"] = player;
        Params["Started"] = started;
        Params["GameOver"] = gameOver;
        Params["JumpsByPlayer"] = JumpsByPlayer;
        Params["PlayersPlaying"] = PlayersPlaying;
        Params["Jumps"] = Jumps;
        Params["Sorted"] = Sorted;
    }
    public Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>> PiecesByPlayer
    {
        get{ return piecesByPlayer; }
    }
    public override void SetPlayers(params IDominoPlayer<int>[] players)
    {
        base.SetPlayers(players);
        Params["Players"] = players;
        Params["PlayersPlaying"] = PlayersPlaying;
        foreach(var player in players)
            JumpsByPlayer[player] = 0;
    }
    public virtual void SetSorterPieces(Action<Dictionary<string,object>> SorterPieces)
    {
        this.SorterPieces = SorterPieces;
    }
    public virtual void SortPieces()
    {
        SorterPieces(Params);
        Sorted = true;
        Params["Sorted"] = Sorted;
    }
    public virtual void SetSorter(Func<Dictionary<string,object>,IDominoPiece<int>[],IDominoPiece<int>[]> Sorter)
    {
        this.Sorter = Sorter;
    }
    public virtual void SetComparer(Func<IDominoPiece<int>,IDominoPiece<int>,int> Comparer)
    {
        Params["Comparer"] = Comparer;
        this.Comparer = Comparer;
    }
    public IDominoPiece<int>[] Sort(IDominoPiece<int>[] Pieces)
    {
        return Sorter(Params,Pieces);
    }
    public int Compare(IDominoPiece<int> piece1, IDominoPiece<int> piece2)
    {
        return Comparer(piece1, piece2);
    }
    public override void Update()
    {
        if(gameOver)
            return;
        if(!started)
            throw new InvalidOperationException("No se ha inicializado el juego");
        lastMovement = Updater(Params);
        Params["LastMovement"] = lastMovement;
        if(lastMovement.Pieces != null)
        {
            Jumps = 0;
            Params["Jumps"] = Jumps;
        }
        else
        {
            Jumps++;
            Params["Jumps"] = Jumps;
            JumpsByPlayer[currentPlayer]++;
        }
        gameOver = GameOverer(Params);
        Params["GameOver"] = gameOver;
    }
    public override bool GameOver()
    {
        return gameOver;
    }
    public override void NextPlayer()
    {
        player = Judge(Params);
        currentPlayer = players[player];
        if(state != null)
            state.ChangePlayer();
        Params["CurrentPlayers"] = CurrentPlayers;
        Params["Player"] = player;
    }
    public override void FirstMovement()
    {
        Params["PiecesByPlayer"] = PiecesByPlayer;
        state = GameStarter(Params);
        Params["State"] = state;
        lastMovement = new DominoMovement<int>(state.Initials,new[] { 0 },Players[player].Name);
        Params["LastMovement"] = lastMovement;
    }
    public override IDominoPiece<int>[] ShowPieces(params string[] players)
    {
        List<IDominoPiece<int>> aux = new List<IDominoPiece<int>>();
        foreach(var player in this.players)
            if(players.Contains(player.Name))
                foreach(var piece in piecesByPlayer[player])
                    aux.Add(piece);
        IDominoPiece<int>[] result = new IDominoPiece<int>[aux.Count];
        Array.Copy(aux.ToArray(),result,result.Length);
        return Sort(result);
    }
    public override void Start()
    {
        if(GameOverer == null)
            throw new InvalidOperationException("No se ha determinado una forma de terminar el juego");
        if(GameStarter == null)
            throw new InvalidOperationException("No se ha determinado una forma de comenzar el juego");
        if(Judge == null)
            throw new InvalidOperationException("No se ha determinado una forma de computar los turnos de los jugadores");
        if(Updater == null)
            throw new InvalidOperationException("No se ha determinado una forma de desarrollar e juego");
        if(Starter == null)
            throw new InvalidOperationException("No se ha determinado una forma de inicializar el juego");
        if(Sorter == null)
            throw new InvalidOperationException("No se ha determinado una forma de ordenar las fichas");
        if(Controler == null)
            throw new InvalidOperationException("No se ha determinado una forma de determinar la compatibilidad de  las fichas");
        if(Comparer == null)
            throw new InvalidOperationException("No se ha determinado una forma de comparar las fichas");
        if(WinnerComputer == null)
            throw new InvalidOperationException("No se ha determinado una forma de computar los ganadores del juego");
        if(SorterPieces == null)
            throw new InvalidOperationException("No se ha determinado una forma de repartir las fichas");
        Params["Controler"] = Controler;
        Params["Comparer"] = Comparer;
        started = true;
        Params["Started"] = true;
        SorterPieces.Invoke(Params);
        player = Starter(Params);
        Params["Player"] = player;
        currentPlayer = players[player];
        Params["CurrentPlayers"] = CurrentPlayers;
        Params["FirstMovement"] = FirstMovement;
        Params["Holder"] = ShowPieces;
    }
}