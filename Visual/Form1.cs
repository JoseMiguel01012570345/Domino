using System.Collections.Generic;
using Logic;

namespace Visual;

public partial class Form1 : Form
{
    Graphics graphics;
    Dictionary<string,string> InfoPieces;
    VisualClassicDominoState State;
    List<Tuple<TextBox,Label,ComboBox,Label,ComboBox, Label, Button>> PlayersInGame;
    Dictionary<string,Func<int,int,bool>> CompatibiltyControlersModes;
    Dictionary<string,Func<Dictionary<string,object>,IDominoState<int>>> GameStartersModes;
    Dictionary<string,Func<IDominoPiece<int>,IDominoPiece<int>,int>> ComparersModes;
    Dictionary<string,Func<Dictionary<string,object>,int>> StartersModes;
    Dictionary<string,Func<Dictionary<string,object>,bool>> ModesGameOver;
    Dictionary<string,Func<Dictionary<string,object>,int>> NextPlayerModes;
    Dictionary<string,Func<Dictionary<string,object>,IDominoPiece<int>[],IDominoPiece<int>[]>> Pieces_Sorters_Function;
    Dictionary<string,Action<Dictionary<string,object>>> Sorters_Pieces_Function;
    Dictionary<string,Func<Dictionary<string,object>,DominoMovement<int>>> UpdatersModes;
    Dictionary<string,Func<Dictionary<string,object>,IDominoPlayer<int>[]>> WinnerComputerModes;
    int StartHeight;
    int CurrentMaxNumberOfPieces;
    public Form1()
    {
        InitializeComponent();
        //Creamos el lienzo de dibujo
        graphics = BoxPainter.CreateGraphics();

        //Diccionario con los tipos de fichas existentes
        InfoPieces = new Dictionary<string, string>();
        InfoPieces["Ficha clasica de domino"] = ClassicDominoPiece.Description;
        //mostramos una descripcion del valor por default
        PiecesInfo.Text = InfoPieces[PiecesTypes.Text];

        //Lista con los parametros de los jugadores que jugaran
        PlayersInGame = new List<Tuple<TextBox, Label, ComboBox, Label, ComboBox, Label ,Button>>();

        //Diccionario con las formas de enlazar fichas implementadas
        CompatibiltyControlersModes = new Dictionary<string, Func<int, int, bool>>();
        CompatibiltyControlersModes["Mismos numeros en las caras"] = Controlers.NormalControler;
        CompatibiltyControlersModes["Sumas pares"] = Controlers.PairSumControler;
        CompatibiltyControlersModes["Sumas impares"] = Controlers.NoPairSumControler;
        CompatibiltyControlersModes["Sumas multiplo de 6"] = Controlers.SixMultipleControler;

        //Diccionario con las formas de comenzar el juego implementadas
        GameStartersModes = new Dictionary<string, Func<Dictionary<string,object>, IDominoState<int>>>();
        GameStartersModes["Salida al doble 6"] = GameStarters.ClassicDoubleSixGameStarter;
        GameStartersModes["Salida por decision del jugador"] = GameStarters.ClassicDoubleNineStarter;

        //Diccionario con las formas de comparar fichas implementadas
        ComparersModes = new Dictionary<string, Func<IDominoPiece<int>, IDominoPiece<int>, int>>();
        ComparersModes["Mayor numero de puntos"] = Comparers.ClassicCompare;

        //Diccionario con las formas de seleccionar el primer jugador implemetadas
        StartersModes = new Dictionary<string, Func<Dictionary<string,object>, int>>();
        StartersModes["Comienza el doble mas alto"] = Starters.ClassicDoubleSixStarter;
        StartersModes["Se decide al azar"] = Starters.ClassicDoubleNineStarter;

        //Diccionario con las formas de determinar si el juego ha terminado implementadas
        ModesGameOver = new Dictionary<string, Func<Dictionary<string,object>, bool>>();
        ModesGameOver["Modo clasico de domino"] = GameOverers.ClassicGameOver;
        ModesGameOver["Cantidad de pases"] = GameOverers.GameOvererByJump;

        //Diccionario con las formas de determinar los turnos de los jugadores implementadas
        NextPlayerModes = new Dictionary<string, Func<Dictionary<string,object>, int>>();
        NextPlayerModes["Modo clasico de domino"] = Judgers.ClassicNextPlayer;
        NextPlayerModes["Variante_1"] = Judgers.JudgerJose;
        NextPlayerModes["Variante_2(corrido)"] = Judgers.JudgerRuner;

        //Diccionario con las formas de ordenar las fichas
        Pieces_Sorters_Function = new Dictionary<string, Func<Dictionary<string,object>,IDominoPiece<int>[], IDominoPiece<int>[]>>();
        Pieces_Sorters_Function["Mayor numero en las caras"] = Sorters.ClassicSort;

        //Diccionario con las formas de repartir las fichas
        Sorters_Pieces_Function = new Dictionary<string, Action<Dictionary<string,object>>>();
        Sorters_Pieces_Function["Modo clasico de domino"] = PiecesSorters.ClassicStaticSortPieces;

        //Diccionario con las formas de desarrollar el juego
        UpdatersModes = new Dictionary<string, Func<Dictionary<string,object>, DominoMovement<int>>>();
        UpdatersModes["Modo clasico de domino"] = Updaters.ClassicUpdate;

        //Diccionario con las formas de computar el gandor del juego
        WinnerComputerModes = new Dictionary<string, Func<Dictionary<string,object>, IDominoPlayer<int>[]>>();
        WinnerComputerModes["Modo clasico de domino"] = WinnerComputers.ClassicWinnerComputer;
        WinnerComputerModes["Menos pasado"] = WinnerComputers.WinnerComputer;
        WinnerComputerModes["Menos fichas en mano"] = WinnerComputers.WinnerComputerPieces;

        //Parametros iniciales
        PiecesByPlayer.Enabled = false;
        StepButton.Enabled = false;
        StartHeight = 80;
        CurrentMaxNumberOfPieces = 5;
    }
    private void Paint_Pieces()
    {
        int posy = 200;
        int posx = 50;
        foreach(var piece in WorkSpace.game.PiecesByPlayer[WorkSpace.game.Players[0]])
        {
            VisualClassicDominoPiece Piece = ((ClassicDominoPiece)piece).GetVisual(25,posx,posy);
            Piece.Paint(graphics);
            posy += 25;
        }
        graphics.DrawString(WorkSpace.game.Players[0].Name,this.Font,Brushes.White,new PointF(posx,posy));
        posx = 350;
        posy = 550;
        foreach(var piece in WorkSpace.game.PiecesByPlayer[WorkSpace.game.Players[1]])
        {
            VisualClassicDominoPiece Piece = ((ClassicDominoPiece)piece).GetVisual(25,posx,posy);
            Piece.SetRotation(90);
            Piece.Paint(graphics);
            posx += 25;
        }
        graphics.DrawString(WorkSpace.game.Players[1].Name,this.Font,Brushes.White,new PointF(posx,posy));
        try
        {
            posx = 950;
            posy = 200;
            foreach(var piece in WorkSpace.game.PiecesByPlayer[WorkSpace.game.Players[2]])
            {
                VisualClassicDominoPiece Piece =((ClassicDominoPiece)piece).GetVisual(25,posx,posy);
                Piece.SetRotation(180);
                Piece.Paint(graphics);
                posy += 25;
            }
        }
        catch(Exception ex)
        {
            return;
        }
        graphics.DrawString(WorkSpace.game.Players[2].Name,this.Font,Brushes.White,new PointF(posx,posy));
        posx = 350;
        posy = 50;
        try
        {
            foreach(var piece in WorkSpace.game.PiecesByPlayer[WorkSpace.game.Players[3]])
            {
                VisualClassicDominoPiece Piece = ((ClassicDominoPiece)piece).GetVisual(25,posx,posy);
                Piece.SetRotation(270);
                Piece.Paint(graphics);
                posx += 25;
            }
            graphics.DrawString(WorkSpace.game.Players[3].Name,this.Font,Brushes.White,new PointF(posx,posy));
        }
        catch(Exception ex)
        {
            return;
        }
    }
    private void StartButton_Click(object sender, EventArgs e)
    {
        try
        {
            int maxNumberOfPiecesFaces = int.Parse(Max_NumberOfPiecesFaces.Text);
            if(maxNumberOfPiecesFaces < 4 || maxNumberOfPiecesFaces > 10)
            {
                MessageBox.Show("Valor invalido de la propiedad");
                return;
            }
            WorkSpace.game = new ClassicDomino(maxNumberOfPiecesFaces + 1);
            CurrentMaxNumberOfPieces = maxNumberOfPiecesFaces + 1;
        }
        catch(Exception ex)
        {
            MessageBox.Show("Argumento invalido");
            return;
        }
        if(PlayersInGame.Count == 0)
        {
            MessageBox.Show("Deben existir jugadores para jugar");
            return;
        }
        DominoPlayer<int>[] PlayersToPlay = new DominoPlayer<int>[PlayersInGame.Count];
        for(int i = 0; i < PlayersInGame.Count; i++)
        {
            if(String.IsNullOrEmpty(PlayersInGame[i].Item1.Text))
            {
                MessageBox.Show("Todos los jugadores deben tener un nombre");
                return;
            }
            PlayersToPlay[i] = new DominoPlayer<int>(PlayersInGame[i].Item1.Text);
            WorkSpace.SetSelecter(PlayersInGame[i].Item3.Text,PlayersToPlay[i]);
            WorkSpace.SetStrategy(PlayersInGame[i].Item5.Text,PlayersToPlay[i]);
        }
        WorkSpace.game.SetPlayers(PlayersToPlay);
        try
        {
            int numberOfPiecesByPlayer = int.Parse(PiecesByPlayer.Text);
            if(numberOfPiecesByPlayer > ((WorkSpace.game.MaxNumberOfPieces * (WorkSpace.game.MaxNumberOfPieces + 1) / 2)) / PlayersToPlay.Length)
            {
                MessageBox.Show("Fichas insuficientes para repartir");
                return;
            }
            if(numberOfPiecesByPlayer < 3)
            {
                MessageBox.Show("Muy pocas fichas para jugar");
                return;
            }
            PiecesSorters.TotalPieces = numberOfPiecesByPlayer;
        }
        catch(Exception ex)
        {
            MessageBox.Show("El valor pasado para la cantidad de fichas por jugador no es un argumento valido");
            return;
        }
        if(CompatibiltyControlersModes.Keys.Contains(CompatibilityControlers.Text.ToString()))
            WorkSpace.game.SetControler(CompatibiltyControlersModes[CompatibilityControlers.Text.ToString()]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido 'Criterio de enlaze de fichas'");
            return;
        }
        if(GameStartersModes.Keys.Contains(StartersGames.Text.ToString()))
            WorkSpace.game.SetGameStarter(GameStartersModes[StartersGames.Text.ToString()]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Modo de salir'");
            return;
        }
        if(ComparersModes.Keys.Contains(GamesComparers.Text))
            WorkSpace.game.SetComparer(ComparersModes[GamesComparers.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Modo de comparar'");
            return;
        }
        if(StartersModes.Keys.Contains(StartersGamesModes.Text))
            WorkSpace.game.SetStarter(StartersModes[StartersGamesModes.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Modo de iniciar'");
            return;
        }
        if(ModesGameOver.Keys.Contains(GameOversModes.Text))
            WorkSpace.game.SetGameOver(ModesGameOver[GameOversModes.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Modo de terminar'");
            return;
        }
        if(NextPlayerModes.Keys.Contains(NextPlayer.Text))
            WorkSpace.game.SetJudger(NextPlayerModes[NextPlayer.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido de la propiedade 'Orden de los turnos'");
            return;
        }
        if(Pieces_Sorters_Function.Keys.Contains(Soters_Pieces.Text))
            WorkSpace.game.SetSorter(Pieces_Sorters_Function[Soters_Pieces.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Ordenar fichas por'");
            return;
        }
        if(Sorters_Pieces_Function.Keys.Contains(SorterPiecesByPlayer.Text))
            WorkSpace.game.SetSorterPieces(Sorters_Pieces_Function[SorterPiecesByPlayer.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido de la propiedad 'Modo de repartir las fichas'");
            return;
        }
        if(UpdatersModes.Keys.Contains(UpdatersMode.Text))
            WorkSpace.game.SetUpdater(UpdatersModes[UpdatersMode.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Modo de juego'");
            return;
        }
        if(WinnerComputerModes.Keys.Contains(WinnerComputerMode.Text))
            WorkSpace.game.SetWinnerComputer(WinnerComputerModes[WinnerComputerMode.Text]);
        else
        {
            MessageBox.Show("Argumento no encontrado como argumento valido para la propiedad 'Determinar el ganador por'");
            return;
        }
        try
        {
            WorkSpace.Start();
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            return;
        }
        SuspendLayout();
        Controls.Remove(PiecesInfo);
        Controls.Remove(PiecesTypes);
        Controls.Remove(StartButton);
        Controls.Remove(TypePieces);
        Controls.Remove(PiecesByPlayer);
        Controls.Remove(NumberOfPiecesByPlayer);
        Controls.Remove(MaxNumberOfPiecesFaces);
        Controls.Remove(Max_NumberOfPiecesFaces);
        Controls.Remove(CompatibilityControlers);
        Controls.Remove(CompatibilityPieces);
        Controls.Remove(StartersGames);
        Controls.Remove(Starters_Games);
        Controls.Remove(GamesComparers);
        Controls.Remove(ComparersGames);
        Controls.Remove(StartersGamesModes);
        Controls.Remove(SelecterGamesStarters);
        Controls.Remove(GameOversModes);
        Controls.Remove(OversModes);
        Controls.Remove(NextPlayer);
        Controls.Remove(NextPlayerMode);
        Controls.Remove(Soters_Pieces);
        Controls.Remove(Pieces_Sorters);
        Controls.Remove(SorterPiecesByPlayer);
        Controls.Remove(Sorter_PiecesByPlayer);
        Controls.Remove(UpdatersMode);
        Controls.Remove(Updaters_Mode);
        Controls.Remove(WinnerComputerMode);
        Controls.Remove(Winner_ComputerMode);
        foreach(var item in PlayersInGame)
        {
            Controls.Remove(item.Item1);
            Controls.Remove(item.Item2);
            Controls.Remove(item.Item3);
            Controls.Remove(item.Item4);
            Controls.Remove(item.Item5);
            Controls.Remove(item.Item6);
            Controls.Remove(item.Item7);
        }
        Controls.Remove(Players);
        Controls.Remove(PlayersCreator);
        Controls.Add(StepButton);
        Controls.Add(BoxPainter);
        Controls.Add(GameStartButton);
        Controls.Add(GameInfo);
        ResumeLayout();
    }
    private void GameStartButton_Click(object sender, EventArgs e)
    {
        if(WorkSpace.game.GameOver())
        {
            WorkSpace.Restart(CurrentMaxNumberOfPieces);
            StartButton_Click(sender,e);
            return;
        }
        StepButton.Enabled = true;
        GameStartButton.Enabled = false;
        BoxPainter.Refresh();
        WorkSpace.Update();
        State = new VisualClassicDominoState((ClassicDominoState)WorkSpace.game.State,graphics,25,BoxPainter.Width / 2, BoxPainter.Height / 2);
        State.Show();
        GameInfo.Text = "Ha comenzado el jugador " + WorkSpace.game.CurrentPlayers[0].Name;
    }
    private void StepButton_Click(object sender, EventArgs e)
    {
        if(!WorkSpace.game.GameOver())
        {
            WorkSpace.game.NextPlayer();
            ClassicDominoPiece[] pieces = (ClassicDominoPiece[])WorkSpace.game.State.PiecesTops;
            int[] tops_left = WorkSpace.game.State.PiecesTops[0].Tops;
            int[] tops_right = WorkSpace.game.State.PiecesTops[1].Tops;
            WorkSpace.game.Update();
            if(WorkSpace.game.LastMovement.Pieces != null)
            {
                IDominoPiece<int> Piece = WorkSpace.game.State.Relator.FatherOf(WorkSpace.game.LastMovement.Pieces[0].ToString());
                State.AddPiece(WorkSpace.game.LastMovement.Pieces[0],Piece,WorkSpace.game.Controler);
            }
            GameInfo.Text = WorkSpace.game.LastMovement.ToString();
            State.Show();
        }
        else
        {
            if(WorkSpace.game.Winners.Length > 1)
            {
                string winners = "";
                foreach(var player in WorkSpace.game.Winners)
                    winners += player.Name +" ";
                GameInfo.Text = "El juego ha quedado empatado entre los jugadores " + winners;
            }
            else
            {
                GameInfo.Text = "El juego lo ha ganado " + WorkSpace.game.Winners[0].Name;
            }
            StepButton.Enabled = false;
            GameStartButton.Enabled = true;
        }
    }
    private void PiecesTypes_SelectedValueChanged(object sender, EventArgs e)
    {
        PiecesInfo.Text = InfoPieces[PiecesTypes.Text];
    }
    private void PlayersCreator_Click(object sender, EventArgs e)
    {
        PiecesByPlayer.Enabled = true;
        PiecesByPlayer.Text = "";
        //creamos el espacio para el nombre del jugador
        TextBox textBox = new TextBox();
        textBox.Location = new Point(600,StartHeight);
        textBox.Name = "Player_" + PlayersInGame.Count.ToString();
        textBox.Size = new Size(100,50);

        //creamos el label con la informacion de la funcion del texbox
        Label Name = new Label();
        Name.Location = new Point(600,StartHeight - 20);
        Name.Size = new Size(100,50);
        Name.Text = "Nombre:";
        Name.Name = "Name_" + textBox.Name;
        Name.Visible = true;

        //creamos el espacio para que seleccione la forma en que escogera fichas
        ComboBox SelectMode = new ComboBox();
        SelectMode.Location = new Point(710,StartHeight - 15);
        SelectMode.Name = "SelectMode_" + textBox.Name;
        SelectMode.FormattingEnabled = true;
        SelectMode.Text = "Aleatorio";
        SelectMode.Items.AddRange(new object[]
        {
            "Aleatorio",
            "Bota_Gorda"
        });
        SelectMode.Size = new Size(100,50);

        //creamos el label para la informacion de la funcion del combobox
        Label Select = new Label();
        Select.Location = new Point(710,StartHeight -30);
        Select.Name = "Select_" + textBox.Name;
        Select.Text = "Modo de seleccionar fichas";
        Select.Size = new Size(200,50);
        Select.Visible = true;

        //creamos la lista de strategias a usar
        ComboBox comboBox = new ComboBox();
        comboBox.Location = new Point(710,StartHeight + 40);
        comboBox.Name = "Strategie_" + textBox.Name;
        comboBox.FormattingEnabled = true;
        comboBox.Text = "Aleatorio";
        comboBox.Items.AddRange(new object[]
        {
            "Aleatorio",
            "Bota_Gorda",
            "Heuristico",
            "Agachado",
            "Simulador"
        });
        comboBox.Size = new Size(100, 40);
        //creamos el label con informacion de la funcion del combobox
        Label Strategie = new Label();
        Strategie.Location = new Point(710,StartHeight + 25);
        Strategie.Name = "Strategie_" + textBox.Name;
        Strategie.Text = "Estrategias";
        Strategie.Size = new Size(100,50);
        Strategie.Visible = true;

        //creamos un boton para eliminarlo
        Button button = new Button();
        button.Location = new Point(820,StartHeight);
        button.Name = "Clear_" + textBox.Name;
        button.Text = "Eliminar";
        button.Size = new Size(60,30);
        button.UseVisualStyleBackColor = true;
        button.Click += new EventHandler(Players_Destroyer);

        StartHeight += 130;
        PlayersInGame.Add(new Tuple<TextBox,Label, ComboBox, Label, ComboBox, Label, Button>(textBox, Name, SelectMode, Select, comboBox, Strategie, button));
        Controls.Add(textBox);
        Controls.Add(comboBox);
        Controls.Add(button);
        Controls.Add(SelectMode);
        Controls.Add(Name);
        Controls.Add(Select);
        Controls.Add(Strategie);
    }
    private void Players_Destroyer(object sender, EventArgs e)
    {
        int pos = 0;
        foreach(var item in PlayersInGame)
        {
            if(item.Item7.Name == ((Button)sender).Name)
            {
                Controls.Remove(item.Item1);
                Controls.Remove(item.Item2);
                Controls.Remove(item.Item3);
                Controls.Remove(item.Item4);
                Controls.Remove(item.Item6);
                Controls.Remove(item.Item7);
                Controls.Remove(item.Item5);
                break;
            }
            pos++;
        }   
        PlayersInGame.RemoveAt(pos);
        if(PlayersInGame.Count == 0)
        {
            PiecesByPlayer.Text = "";
            PiecesByPlayer.Enabled = false;
        }
        StartHeight -= 130;
        Adjust_Players(PlayersInGame);
    }
    private void Adjust_Players(List<Tuple<TextBox, Label,ComboBox, Label, ComboBox, Label, Button>> players)
    {
        SuspendLayout();
        int increment = 80;
        foreach(var item in PlayersInGame)
        {
            item.Item1.Location = new Point(item.Item1.Location.X, increment);
            item.Item2.Location = new Point(item.Item2.Location.X, increment - 20);
            item.Item3.Location = new Point(item.Item3.Location.X, increment - 15);
            item.Item4.Location = new Point(item.Item4.Location.X, increment - 30);
            item.Item5.Location = new Point(item.Item5.Location.X, increment + 40);
            item.Item6.Location = new Point(item.Item6.Location.X, increment + 25);
            item.Item7.Location = new Point(item.Item7.Location.X, increment);
            increment += 130;
        }
        this.Update();
        ResumeLayout();
    }
    private void PiecesByPlayer_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int number_of_pieces = int.Parse(Max_NumberOfPiecesFaces.Text);
            if(((number_of_pieces + 1) * (number_of_pieces + 2)) / 2 > int.Parse(PiecesByPlayer.Text) * PlayersInGame.Count)
            {
                StartersGames.Items.Remove("Salida al doble 6");
                StartersGames.Text = "Salida por decision del jugador";
                StartersGamesModes.Items.Remove("Comienza el doble mas alto");
                StartersGamesModes.Text = "Se decide al azar";
            }
            else
            {
                StartersGames.Items.Add("Salida al doble 6");
                StartersGamesModes.Items.Add("Comienza el doble mas alto");
            }
        }
        catch(Exception ex){ }
    }
    private void Max_NumberOfPiecesFaces_TextChanged(object sender, EventArgs e)
    {
        PiecesByPlayer.Text = "";
    }
}