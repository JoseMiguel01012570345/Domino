namespace Logic;
public interface IStarterSelector<T>
{
    //permite elegir de que forma se iniciara el juego
    public void SetStarter(Func<Dictionary<string,object>,int> Starter);
}
public interface IStarter
{
    //permite iniciar un juego
    public void Start();
}