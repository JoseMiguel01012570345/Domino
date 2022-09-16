namespace Logic;
public interface IControlerSelector<T>
{
    //permite elegir la forma de enlazar fichas
    public void SetControler(Func<T,T,bool> Controler);
}