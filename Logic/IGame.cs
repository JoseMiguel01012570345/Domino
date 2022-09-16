namespace Logic;
public interface IGameModesSelector<T>:IGameOverSelector<T>,IJudgeSelector<T>,IUpdaterSelector<T>,IStarterSelector<T>,IComputerWinnerSelector<T>
{
    //permite determinar como se hace la primera jugada
    public void SetGameStarter(Func<Dictionary<string,object>,IDominoState<T>> GameStarter);
}

//caracteristicas de todo juego
public interface IGame<T>:IGameOverer,INextPlayerSelector,IUpdater,IStarter
{
    //debe saber si ya se ha iniciado
    public bool Started{get;}
    //debe saber cual fue la ultima jugada realizada
    public DominoMovement<T> LastMovement{get;}
    //permite realizar la primera jugada
    public void FirstMovement();
}