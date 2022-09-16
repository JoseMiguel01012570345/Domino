using System.Collections.Generic;
namespace Logic;
//ordenadores de fichas
public static class Sorters
{
    //Parametros
    //---- Metodo comparador del juego
    public static IDominoPiece<int>[] ClassicSort(Dictionary<string,object> Params, IDominoPiece<int>[] Pieces)
    {
        for(int i = 0; i < Pieces.Length; i++)
        {
            for(int j = i; j < Pieces.Length; j++)
            {
                if(((Func<IDominoPiece<int>,IDominoPiece<int>,int>)Params["Comparer"]).Invoke(Pieces[i],Pieces[j]) < 0)
                {
                    IDominoPiece<int> temp = Pieces[i];
                    Pieces[i] = Pieces[j];
                    Pieces[j] = temp;
                }
            }
        }
        return Pieces;
    }
}