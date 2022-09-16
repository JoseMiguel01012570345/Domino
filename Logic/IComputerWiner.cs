namespace Logic;
public interface IComputerWinnerSelector<T>
{
    //permite elegir una forma de computar un ganador
    public void SetWinnerComputer(Func<Dictionary<string,object>,IDominoPlayer<T>[]> WinnerComputer);
}