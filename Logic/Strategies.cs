using System;
using System.Collections.Generic;
namespace Logic;
public class ClassicDominoRandomStrategy:IStrategy<int>
{
    Random random;
    public ClassicDominoRandomStrategy()
    {
        random = new Random();
    }
    public DominoMovement<int> ExecuteStrategy(Dictionary<string,object> Params, string Player)
    {
        if(((IDominoState<int>)Params["State"]) == null)
        {
            IDominoPiece<int>[] pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
            IDominoPiece<int> piece =pieces[random.Next(pieces.Length)];
            return new DominoMovement<int>(new[] { piece }, new[] { 0 } , Player);
        }
        List<Tuple<IDominoPiece<int>,int>> ValidsPieces = new List<Tuple<IDominoPiece<int>,int>>();
        IDominoPiece<int>[] Pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
        foreach(var top in ((IDominoState<int>)Params["State"]).Tops)
        {
            foreach(var piece in Pieces)
            {
                foreach(var piece_top in piece.Tops)
                {
                    if(((Func<int,int,bool>)Params["Controler"]).Invoke(piece_top,top))
                    {
                        ValidsPieces.Add(new Tuple<IDominoPiece<int>, int>(piece,top));
                        break;
                    }
                }
            }
        }
        if(ValidsPieces.Count == 0)
            return new DominoMovement<int>(null,((IDominoState<int>)Params["State"]).Tops,Player);
        int piece_pos = random.Next(ValidsPieces.Count);
        return new DominoMovement<int>(new[]{ValidsPieces[piece_pos].Item1},new[]{ValidsPieces[piece_pos].Item2},Player);

    }
}
public class ClassicDominoThrowFatStrategy:IStrategy<int>
{
    public DominoMovement<int> ExecuteStrategy(Dictionary<string,object> Params,string Player)
    {
        IDominoPiece<int>[] Pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
        if(((IDominoState<int>)Params["State"]) == null)
            return new DominoMovement<int>(new[] { Pieces[0] },new[] { 0 } , Player);
        for(int i = 0; i < Pieces.Length; i++)
        {
            foreach(var top in ((IDominoState<int>)Params["State"]).Tops)
            {
                if(Pieces[i].Contains(top,((Func<int,int,bool>)Params["Controler"])))
                {
                    IDominoPiece<int> result = Pieces[i];
                    return new DominoMovement<int>(new[] { result }, new[] { top },Player);
                }
            }
        }
        return new DominoMovement<int>(null, new[] { -1 } , Player);
    }
}
public class ClassicDoubleSixAliHeuristicStrategy:IStrategy<int>
{
    Random random;
    public ClassicDoubleSixAliHeuristicStrategy()
    {
        random = new Random();
    }
    public DominoMovement<int> ExecuteStrategy(Dictionary<string, object> Params,string Player)
    {
        IDominoPiece<int>[] Pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
        //si no se ha iniciado el juego
        if(((IDominoState<int>)Params["State"]) == null)
        {
            //jugamos al azar
            IDominoPiece<int> piece = Pieces[random.Next(Pieces.Length)];
            return new DominoMovement<int>(new[] { piece }, new[] { 0 }, Player);
        }
        //obtenemos los extremos por donde podemos jugar
        int topLeft = ((IDominoState<int>)Params["State"]).Tops[0];
        int topRight = ((IDominoState<int>)Params["State"]).Tops[1];
        //obtenemos las fichas validas
        List<IDominoPiece<int>> ValidsPieces = new List<IDominoPiece<int>>();
        foreach(var piece in Pieces)
            if(piece.Contains(topLeft,((Func<int,int,bool>)Params["Controler"])) || piece.Contains(topRight,((Func<int,int,bool>)Params["Controler"])))
                ValidsPieces.Add(piece);
        //contamos cuantas fichas de cada numero de las que tenemos
        Dictionary<int,int> PiecesByTypes = new Dictionary<int, int>();
        foreach(var piece in Pieces)
        {
            if(!PiecesByTypes.Keys.Contains(((ClassicDominoPiece)piece).Left))
                PiecesByTypes[((ClassicDominoPiece)piece).Left] = 1;
            else
                PiecesByTypes[((ClassicDominoPiece)piece).Left]++;
            if(!Utils.IsDouble((ClassicDominoPiece)piece) && !PiecesByTypes.Keys.Contains(((ClassicDominoPiece)piece).Right))//comprobar que la ficha no es doble
                PiecesByTypes[((ClassicDominoPiece)piece).Right] = 1;
            else if(!Utils.IsDouble((ClassicDominoPiece)piece))
                PiecesByTypes[((ClassicDominoPiece)piece).Right]++;
        }
        //si tenemos mas de 4 fichas del mismo tipo 
        foreach(var top in PiecesByTypes.Keys)
            if(PiecesByTypes[top] > 4)
                foreach(var piece in ValidsPieces)
                    if(piece.Contains(top,(a,b) => { return a == b; }) && !Utils.IsDouble((ClassicDominoPiece)piece))
                        if(piece.Contains(topLeft,((Func<int,int,bool>)Params["Controler"])) && topLeft != top)
                            return new DominoMovement<int>(new[] { piece },new[] { topLeft }, Player);//ponemos el juego a las dos cabezas por el valor que nos favorece
                        else if(piece.Contains(topRight,((Func<int,int,bool>)Params["Controler"])) && topRight != top)
                            return new DominoMovement<int>(new[] { piece },new[] { topRight }, Player);
        //vemos que fichas se han jugado hasta el momento
        IDominoPiece<int>[] PiecesInGame = ((IDominoState<int>)Params["State"]).PiecesPlayedTillNow;
        //vemos las fichas que quedan en juego con respecto a las que tenemos
        Dictionary<int,int> PiecesRemains = new Dictionary<int, int>();
        foreach(var top in PiecesByTypes.Keys)
        {
            //descontamos las fichas en mesa
            PiecesRemains[top] = ((int)Params["MaxNumberOfPieces"]);
            foreach(var piece in PiecesInGame)
                if(piece.Contains(top,((Func<int,int,bool>)Params["Controler"])))
                    PiecesRemains[top]--;
            //descontamos las que tenemos
            PiecesRemains[top] -= PiecesByTypes[top];
            //hay que descontar los dobles que esten en juego todavia
            if(!Utils.ContainsDouble(top,PiecesInGame) && !Utils.ContainsDouble(top,Pieces))
                PiecesRemains[top]--;
        }
        //jugamos las fichas para quedarnos con un lugar fijo por donde poder jugar
        foreach(var piece in ValidsPieces)
        {
            if(PiecesRemains.Keys.Contains(((ClassicDominoPiece)piece).Left))
            {
                //si no quedan mas de esas fichas
                if(PiecesRemains[((ClassicDominoPiece)piece).Left] == 0)
                {
                    if(((Func<int,int,bool>)Params["Controler"]).Invoke(((ClassicDominoPiece)piece).Left,topLeft))
                        return new DominoMovement<int>(new[] { piece }, new[] { topLeft }, Player);
                    return new DominoMovement<int>(new[] { piece }, new[] { topRight }, Player);
                }
            }
            if(PiecesRemains[((ClassicDominoPiece)piece).Right] == 0)
            {
                if(((Func<int,int,bool>)Params["Controler"]).Invoke(((ClassicDominoPiece)piece).Right,topLeft))
                    return new DominoMovement<int>(new[] { piece }, new[] { topLeft }, Player);
                return new DominoMovement<int>(new[] { piece }, new[] { topRight }, Player);
            }
        }
        //si podemos jugar para quedarnos fijos lo hacemos
        foreach(var piece in ((IDominoState<int>)Params["State"]).PiecesTops)
        {
            //si la ficha no la puso este jugador
            if(((IDominoState<int>)Params["State"]).Relator.OwnerOf(piece.ToString()) != Player)
            {
                foreach(var valid_piece in ValidsPieces)
                {
                    if(PiecesRemains.Keys.Contains(((ClassicDominoPiece)valid_piece).Left))
                    {
                        //comparamos para ver si mantenemos la ventaja
                        if(PiecesRemains[((ClassicDominoPiece)valid_piece).Left] < PiecesByTypes[((ClassicDominoPiece)valid_piece).Left] - 2)
                        {
                            if(((Func<int,int,bool>)Params["Controler"]).Invoke(((ClassicDominoPiece)valid_piece).Left,topLeft))
                                return new DominoMovement<int>(new[] { valid_piece }, new[] { topLeft }, Player);
                            return new DominoMovement<int>(new[] { valid_piece }, new[] { topRight }, Player);
                        }
                    }
                    if(PiecesRemains[((ClassicDominoPiece)valid_piece).Right] < PiecesByTypes[((ClassicDominoPiece)valid_piece).Right] - 2)
                    {
                        if(((Func<int,int,bool>)Params["Controler"]).Invoke(((ClassicDominoPiece)valid_piece).Right,topLeft))
                            return new DominoMovement<int>(new[] { valid_piece }, new[] { topLeft }, Player);
                        return new DominoMovement<int>(new[] { valid_piece }, new[] { topRight }, Player);
                    }       
                }
            }
        }
        //escogemos las fichas que nos dejen con mas fichas del mismo tipo para poder jugar
        List<IDominoPiece<int>> GoodPieces = new List<IDominoPiece<int>>();
        foreach(var piece in ValidsPieces)
        {
            if(PiecesByTypes[((ClassicDominoPiece)piece).Left] == 1 || PiecesByTypes[((ClassicDominoPiece)piece).Left] == 1)
                continue;
            foreach(var top in PiecesByTypes.Keys)
            {
                if(PiecesRemains[top] == 1 && piece.Contains(top, (a,b) => { return a == b; }))
                {
                    if(piece.Contains(topLeft,((Func<int,int,bool>)Params["Controler"])))
                        return new DominoMovement<int>(new[] { piece }, new[] { topLeft }, Player);
                    return new DominoMovement<int>(new[] { piece }, new[] { topRight }, Player);
                }
                if(piece.Contains(top,(a,b) => { return a == b; }) && PiecesByTypes[top] > 1)
                {
                    //si tenemos el doble de esa ficha no la tira para evitar que que le cierren la salida de la ficha 
                    if(!Utils.IsDouble((ClassicDominoPiece)piece) && Utils.ContainsDouble(top, Pieces) && top != 0)
                        continue;
                    if(Utils.IsDouble((ClassicDominoPiece)piece))
                    {
                        if(((Func<int,int,bool>)Params["Controler"]).Invoke(top,topLeft))
                            return new DominoMovement<int>(new[] { piece }, new[] { topLeft }, Player);
                        else
                            return new DominoMovement<int>(new[] { piece }, new[] { topRight }, Player);
                    }
                    GoodPieces.Add(piece);
                    break;
                }
            }
        }
        //si existen este tipo de fichas
        if(GoodPieces.Count > 0)
        {
            foreach(var piece in ((IDominoState<int>)Params["State"]).PiecesTops)
            {
                if(((IDominoState<int>)Params["State"]).Relator.OwnerOf(piece.ToString()) != Player)
                {
                    foreach(var valid_piece in GoodPieces)
                    {
                        if(PiecesRemains.Keys.Contains(((ClassicDominoPiece)valid_piece).Left))
                        {
                            if(PiecesRemains[((ClassicDominoPiece)valid_piece).Left] == 0)
                            {
                                if(((Func<int,int,bool>)Params["Controler"]).Invoke(((ClassicDominoPiece)valid_piece).Left,topLeft))
                                    return new DominoMovement<int>(new[] { valid_piece }, new[] { topLeft }, Player);
                                return new DominoMovement<int>(new[] { valid_piece }, new[] { topRight }, Player);
                            }
                        }
                        if(PiecesRemains[((ClassicDominoPiece)valid_piece).Right] == 0)
                        {
                            if(((Func<int,int,bool>)Params["Controler"]).Invoke(((ClassicDominoPiece)valid_piece).Right,topLeft))
                                return new DominoMovement<int>(new[] { valid_piece }, new[] { topLeft }, Player);
                            return new DominoMovement<int>(new[] { valid_piece }, new[] { topRight }, Player);
                        }       
                    }
                }
            }   
        }
        foreach(var piece in ValidsPieces)
        {
            if(piece.Contains(topLeft,((Func<int,int,bool>)Params["Controler"])))
                return new DominoMovement<int>(new[] { piece }, new[] { topLeft }, Player);
            if(piece.Contains(topRight,((Func<int,int,bool>)Params["Controler"])))
                return new DominoMovement<int>(new[] { piece }, new[] { topRight }, Player);
        }
        return new DominoMovement<int>(null,new[] { -1 }, Player);
    }
}
public class CrouchedDownClassicDominoPlayerStrategy:IStrategy<int>
{
    public DominoMovement<int> ExecuteStrategy(Dictionary<string, object> Params,string Player)
    {
        IDominoPiece<int>[] Pieces = ((Func<string[],IDominoPiece<int>[]>)Params["Holder"]).Invoke(new[] { Player });
        Dictionary<int,int> Frecuency = new Dictionary<int, int>();
        if(((IDominoState<int>)Params["State"]) == null)
        {
            foreach(var piece in Pieces)
            {
                foreach(var top in piece.Tops)
                {
                    if(!Frecuency.Keys.Contains(top))
                        Frecuency[top] = 1;
                    else
                        Frecuency[top]++;
                }
            }
            int sum = 0;
            foreach(var top in Pieces[0].Tops)
                sum += Frecuency[top];
            for(int i = 0; i < Pieces.Length; i++)
            {
                int aux = 0;
                foreach(var top in Pieces[i].Tops)
                    aux += Frecuency[top];
                if(aux > sum)
                {
                    sum = aux;
                    IDominoPiece<int> temp = Pieces[i];
                    Pieces[i] = Pieces[0];
                    Pieces[0] = temp;
                }
            }
            return new DominoMovement<int>(new[] { Pieces[0] }, new[] { 0 }, Player);
        }
        List<IDominoPiece<int>> ValidsPieces = new List<IDominoPiece<int>>();
        foreach(var piece in Pieces)
        {
            foreach(var top in ((IDominoState<int>)Params["State"]).Tops)
            {
                if(piece.Contains(top,((Func<int,int,bool>)Params["Controler"])))
                {
                    ValidsPieces.Add(piece);
                    break;
                }
            }
        }
        foreach(var piece in Pieces)
        {
            foreach(var top in piece.Tops)
            {
                if(!Frecuency.Keys.Contains(top))
                    Frecuency[top] = 1;
                else
                    Frecuency[top]++;
            }
        }
        if(ValidsPieces.Count == 0)
            return new DominoMovement<int>(null,new[] { -1 }, Player);
        int Sum = 0;
        foreach(var top in Pieces[0].Tops)
            Sum += Frecuency[top];
        for(int i = 0; i < ValidsPieces.Count; i++)
        {
            int aux = 0;
            foreach(var top in ValidsPieces[i].Tops)
                aux += Frecuency[top];
            if(aux > Sum)
            {
                Sum = aux;
                IDominoPiece<int> temp = ValidsPieces[i];
                ValidsPieces[i] = ValidsPieces[0];
                ValidsPieces[0] = temp;
            }
        }
        int topPlayed = 0;
        foreach(var top in ((IDominoState<int>)Params["State"]).Tops)
            if(ValidsPieces[0].Contains(top,((Func<int,int,bool>)Params["Controler"])))
            {
                topPlayed = top;
                break;
            }
        return new DominoMovement<int>(new[] { ValidsPieces[0] }, new[] { topPlayed }, Player);
    }
}
public class SimulatorPlayerStrategy:IStrategy<int>
{
    ClassicDominoTree<int> PieceToPlay(ClassicDominoTree<int> Tree,Dictionary<string,object> Params,IDominoPlayer<int> Player)
    {
        Queue<ClassicDominoTree<int>> Leafs = new Queue<ClassicDominoTree<int>>();
        int player = 0;
        for(int j = 0; j < ((IDominoPlayer<int>[])Params["Players"]).Length; j++)
            if(((IDominoPlayer<int>[])Params["Players"])[j].Equals(Player))
                {
                    player = j;
                    break;
                }
        Leafs.Enqueue(Tree);
        for(int i = 0; i < ((IDominoPlayer<int>[])Params["Players"]).Length; i++)
        {
            player++;
            player %= ((IDominoPlayer<int>[])Params["Players"]).Length;
            List<ClassicDominoTree<int>> states = new List<ClassicDominoTree<int>>();
            while(Leafs.Count > 0)
            {
                if(Leafs.Peek() == null)
                    Leafs.Dequeue();
                else
                {
                    Leafs.Peek().Grow(((IDominoPlayer<int>[])Params["Players"])[player]);
                    states.Add(Leafs.Dequeue());
                }
            }
            foreach(var tree in states)
            {
                ClassicDominoTree<int> state = tree;
                foreach(var child in state.Childs)
                {
                    Leafs.Enqueue(child);
                }
            }
        }
        ClassicDominoTree<int> pieceToPlay = null;
        if(Tree.Childs.Length > 0)
            pieceToPlay = Tree.Childs[0];
        foreach(var node in Tree.Childs)
        {
            if(pieceToPlay == null && node != null)
                pieceToPlay = node;
            else if(node != null)
                if(node.Wins > pieceToPlay.Wins)
                    pieceToPlay = node;
        }
        return pieceToPlay;
    }
    public DominoMovement<int> ExecuteStrategy(Dictionary<string,object> Params,string Player)
    {
        IDominoPlayer<int> p = null;
        foreach(var player in (IDominoPlayer<int>[])Params["Players"])
            if(Player == player.Name)
                p = player;
        if(((IDominoState<int>)Params["State"]) == null)
            return new ClassicDominoThrowFatStrategy().ExecuteStrategy(Params,Player);
        Queue<ClassicDominoTree<int>> Leafs = new Queue<ClassicDominoTree<int>>();
        Node<int> LeftRoot = new Node<int>(
            ((IDominoState<int>)Params["State"]).Tops[0],
            ((IDominoState<int>)Params["State"]).Tops[1],
            ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"]),
            (Func<int,int,bool>)Params["Controler"],
            p,
            ((IDominoState<int>)Params["State"]).PiecesTops[0]);

        Node<int> RightRoot = new Node<int>(
            ((IDominoState<int>)Params["State"]).Tops[0],
            ((IDominoState<int>)Params["State"]).Tops[1],
            ((Dictionary<IDominoPlayer<int>,List<IDominoPiece<int>>>)Params["PiecesByPlayer"]),
            ((Func<int,int,bool>)Params["Controler"]),
            p,
            ((IDominoState<int>)Params["State"]).PiecesTops[1]);
        ClassicDominoTree<int> LeftTree = new ClassicDominoTree<int>(LeftRoot,p);
        ClassicDominoTree<int> RightTree = new ClassicDominoTree<int>(RightRoot,p);
        ClassicDominoTree<int> pieceToPlay = PieceToPlay(LeftTree,Params,p);
        if(pieceToPlay == null)
        {
            ClassicDominoTree<int> a = PieceToPlay(RightTree,Params,p);
            if(a == null)
                return new ClassicDominoThrowFatStrategy().ExecuteStrategy(Params,Player);
            pieceToPlay = a;
        }
        ClassicDominoTree<int> pieceToPlay_other = PieceToPlay(RightTree,Params,p);
        if(pieceToPlay_other != null && pieceToPlay_other.Wins > pieceToPlay.Wins)
            pieceToPlay = pieceToPlay_other;
        int topToPlay = 0;
        foreach(var top in ((IDominoState<int>)Params["State"]).Tops)
            if(pieceToPlay.Value.Piece.Contains(top,(Func<int,int,bool>)Params["Controler"]))
            {
                topToPlay = top;
                break;
            }
        return new DominoMovement<int>(new[] { pieceToPlay.Value.Piece }, new[] { topToPlay }, Player);
    }
}