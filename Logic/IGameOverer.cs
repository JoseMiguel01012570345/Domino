namespace Logic;
public interface IGameOverSelector<T>
{
    //permite elegir la forma en que va a terminar el juego
    public void SetGameOver(Func<Dictionary<string,object>,bool> GameOver);
}
public interface IGameOverer
{
    //permite determinar si el juego ha terminado
    public bool GameOver();
}