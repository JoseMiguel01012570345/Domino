namespace Visual;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        //Propiedades de BoxPainter
        BoxPainter = new PictureBox();
        ((System.ComponentModel.ISupportInitialize)BoxPainter).BeginInit();
        SuspendLayout();
        BoxPainter.Location = new Point(0,0);
        BoxPainter.Name = "BoxPainter";
        BoxPainter.Size = new Size(1040,600);
        BoxPainter.TabIndex = 0;
        BoxPainter.TabStop = false;
        BoxPainter.BackgroundImage = new Bitmap(new Bitmap(@"D:\Proyectos_no_borrar\Yonatan_Proyecto_No_Borrar_New\Visual\BoxPainterImage\Table.jpg"),1040,600);

        //Propiedades de StepButton
        StepButton = new Button();
        StepButton.Location = new Point(1040,400);
        StepButton.Name = "StepButton";
        StepButton.Size = new Size(100,20);
        StepButton.TabIndex = 5;
        StepButton.Text = "Step";
        StepButton.UseVisualStyleBackColor = true;
        StepButton.Click += new EventHandler(StepButton_Click);

        //Propiedades de StartButton
        StartButton = new Button();
        StartButton.Location = new Point(1000,500);
        StartButton.Name = "StartButton";
        StartButton.Size = new Size(100,20);
        StartButton.TabIndex = 1;
        StartButton.Text = "Start";
        StartButton.UseVisualStyleBackColor = true;
        StartButton.Click += new EventHandler(StartButton_Click);

        //Propiedades de GameStartButton
        GameStartButton = new Button();
        GameStartButton.Location = new Point(1050,500);
        GameStartButton.Name = "GameStartButton";
        GameStartButton.Size = new Size(100,50);
        GameStartButton.TabIndex = 4;
        GameStartButton.Text = "Start Game";
        GameStartButton.UseVisualStyleBackColor = true;
        GameStartButton.Click += new EventHandler(GameStartButton_Click);

        //Propiedades de PiecesTypes
        PiecesTypes = new ComboBox();
        PiecesTypes.Location = new Point(0,40);
        PiecesTypes.FormattingEnabled = true;
        PiecesTypes.Items.AddRange(new object[]
        { 
          "Ficha clasica de domino",
        });
        PiecesTypes.Name = "PiecesTypes";
        PiecesTypes.Size = new Size(200,50);
        PiecesTypes.TabIndex = 2;
        PiecesTypes.Text = "Ficha clasica de domino";
        PiecesTypes.SelectedValueChanged += new EventHandler(PiecesTypes_SelectedValueChanged);

        //Propiedades de PiecesInfo
        PiecesInfo = new TextBox();
        PiecesInfo.Location = new Point(1050,0);
        PiecesInfo.Name = "PiecesInfo";
        PiecesInfo.Size = new Size(300,200);
        PiecesInfo.Multiline = true;
        PiecesInfo.TabIndex = 3;

        //Propiedades de TypePieces
        TypePieces = new Label();
        TypePieces.Location = new Point(0,0);
        TypePieces.Name = "TypePieces";
        TypePieces.Text = "Tipos de fichas";
        TypePieces.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);
        TypePieces.Visible = true;
        TypePieces.Size = new Size(200,50);

        //Propiedades de PlayersCreator
        PlayersCreator = new Button();
        PlayersCreator.Location = new Point(940,30);
        PlayersCreator.Name = "PlayersCreator";
        PlayersCreator.Text = "Crear Jugador";
        PlayersCreator.Size = new Size(100,30);
        PlayersCreator.UseVisualStyleBackColor = true;
        PlayersCreator.Click += new EventHandler(PlayersCreator_Click);

        //Propiedades de Players
        Players = new Label();
        Players.Location = new Point(630,0);
        Players.Name = "Players";
        Players.Text = "Jugadores a participar";
        Players.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);
        Players.Visible = true;
        Players.Size = new Size(300,50);
        
        //Propiedades de PiecesByPlayer
        PiecesByPlayer = new TextBox();
        PiecesByPlayer.Location = new Point(0,200);
        PiecesByPlayer.Name = "NumberOfPiecesByPlayer";
        PiecesByPlayer.Size = new Size(100,20);
        PiecesByPlayer.TextChanged += new EventHandler(PiecesByPlayer_TextChanged);

        //Propiedades de NumberOfPiecesByPlayer
        NumberOfPiecesByPlayer = new Label();
        NumberOfPiecesByPlayer.Location = new Point(0,100);
        NumberOfPiecesByPlayer.Name = "NumberOfPiecesByPlayer";
        NumberOfPiecesByPlayer.Text = "Numero de fichas por jugador";
        NumberOfPiecesByPlayer.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);
        NumberOfPiecesByPlayer.Visible = true;
        NumberOfPiecesByPlayer.Size = new Size(300, 100);

        //Propiedades de MaxNumberOfPiecesFaces
        MaxNumberOfPiecesFaces = new Label();
        MaxNumberOfPiecesFaces.Location = new Point(0, 70);
        MaxNumberOfPiecesFaces.Size = new Size(130,70);
        MaxNumberOfPiecesFaces.Name = "MaxNumberOfPiecesFaces";
        MaxNumberOfPiecesFaces.Text = "Maximo numero de las caras de las fichas";
        MaxNumberOfPiecesFaces.Font = this.Font;
        MaxNumberOfPiecesFaces.Visible = true;

        //Propiedades de Max_NumberOfPiecesFaces
        Max_NumberOfPiecesFaces = new TextBox();
        Max_NumberOfPiecesFaces.Location = new Point(140, 70);
        Max_NumberOfPiecesFaces.Size = new Size(100,20);
        Max_NumberOfPiecesFaces.Name = "Max_NumberOfPiecesFaces";
        Max_NumberOfPiecesFaces.Text = "6";
        Max_NumberOfPiecesFaces.TextChanged += new EventHandler(Max_NumberOfPiecesFaces_TextChanged);

        //Propiedades de CompatibilityControlers
        CompatibilityControlers = new ComboBox();
        CompatibilityControlers.Location = new Point(0,290);
        CompatibilityControlers.Size = new Size(190,20);
        CompatibilityControlers.Name = "CompatibilityControlers";
        CompatibilityControlers.FormattingEnabled = true;
        CompatibilityControlers.Text = "Mismos numeros en las caras";
        CompatibilityControlers.Items.AddRange(new object[]
        {
            "Mismos numeros en las caras",
            "Sumas pares",
            "Sumas impares",
            "Sumas multiplo de 6"
        });

        //Propiedades CompatibilityPieces
        CompatibilityPieces = new Label();
        CompatibilityPieces.Location = new Point(0,230);
        CompatibilityPieces.Size = new Size(300,70);
        CompatibilityPieces.Name = "CompatibilityPieces";
        CompatibilityPieces.Visible = true;
        CompatibilityPieces.Text = "Criterio de enlaze fichas";
        CompatibilityPieces.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de StartersGames
        StartersGames = new ComboBox();
        StartersGames.Location = new Point(0,370);
        StartersGames.Size = new Size(190,20);
        StartersGames.Name = "GameStarter";
        StartersGames.Text = "Salida al doble 6";
        StartersGames.FormattingEnabled = true;
        StartersGames.Items.AddRange( new object[]
        {
            "Salida al doble 6",
            "Salida por decision del jugador"
        });

        //Propiedades de Starters_Games
        Starters_Games = new Label();
        Starters_Games.Location = new Point(0,320);
        Starters_Games.Size = new Size(200,50);
        Starters_Games.Name = "Starters_Games";
        Starters_Games.Text = "Modo de salida";
        Starters_Games.Visible = true;
        Starters_Games.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de GamesControlers
        GamesComparers = new ComboBox();
        GamesComparers.Location = new Point(0,450);
        GamesComparers.Size = new Size(190,20);
        GamesComparers.Name = "GameComparers";
        GamesComparers.Text = "Mayor numero de puntos";
        GamesComparers.FormattingEnabled = true;
        GamesComparers.Items.AddRange(new object[]
        {
            "Mayor numero de puntos"
        });

        //Propiedades de ComparersGames
        ComparersGames = new Label();
        ComparersGames.Location = new Point(0,400);
        ComparersGames.Size = new Size(250,50);
        ComparersGames.Name = "ComparersGames";
        ComparersGames.Text = "Modo de comparar";
        ComparersGames.Visible = true;
        ComparersGames.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de StartersGamesModes
        StartersGamesModes = new ComboBox();
        StartersGamesModes.Location = new Point(0,530);
        StartersGamesModes.Size = new Size(190,20);
        StartersGamesModes.Name = "StartersGamesModes";
        StartersGamesModes.Text = "Comienza el doble mas alto";
        StartersGamesModes.FormattingEnabled = true;
        StartersGamesModes.Items.AddRange(new object[]
        {
            "Comienza el doble mas alto",
            "Se decide al azar"
        });

        //Propiedades de SelecterGamesStarters
        SelecterGamesStarters = new Label();
        SelecterGamesStarters.Location = new Point(0,480);
        SelecterGamesStarters.Size = new Size(250,50);
        SelecterGamesStarters.Name = "SelecterGamesStarters";
        SelecterGamesStarters.Text = "Modo de iniciar";
        SelecterGamesStarters.Visible = true;
        SelecterGamesStarters.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de GamesOversModes
        GameOversModes = new ComboBox();
        GameOversModes.Location = new Point(0,610);
        GameOversModes.Size = new Size(190,20);
        GameOversModes.Name = "GameOversModes";
        GameOversModes.Text = "Modo clasico de domino";
        GameOversModes.FormattingEnabled = true;
        GameOversModes.Items.AddRange(new object[]
        {
            "Modo clasico de domino",
            "Cantidad de pases"
        });

        //Propiedades de OversModes
        OversModes = new Label();
        OversModes.Location = new Point(0,560);
        OversModes.Size = new Size(250,50);
        OversModes.Name = "OversModes";
        OversModes.Text = "Modo de terminar";
        OversModes.Visible = true;
        OversModes.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de NextPlayer
        NextPlayer = new ComboBox();
        NextPlayer.Location = new Point(350,40);
        NextPlayer.Size = new Size(190,20);
        NextPlayer.Name = "NextPlayer";
        NextPlayer.Text = "Modo clasico de domino";
        NextPlayer.FormattingEnabled = true;
        NextPlayer.Items.AddRange(new object[]
        {
            "Modo clasico de domino",
            "Variante_1",
            "Variante_2(corrido)",
            "Variante_3(justa)"
        });

        //Propiedades de NextPlayerMode
        NextPlayerMode = new Label();
        NextPlayerMode.Location = new Point(350,0);
        NextPlayerMode.Size = new Size(250,50);
        NextPlayerMode.Name = "NextPlayerMode";
        NextPlayerMode.Text = "Orden de los turnos";
        NextPlayerMode.Visible = true;
        NextPlayerMode.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de Sorters_Pieces
        Soters_Pieces = new ComboBox();
        Soters_Pieces.Location = new Point(350,120);
        Soters_Pieces.Size = new Size(190,20);
        Soters_Pieces.Name = "Sosters_Pieces";
        Soters_Pieces.Text = "Mayor numero en las caras";
        Soters_Pieces.FormattingEnabled = true;
        Soters_Pieces.Items.AddRange(new object[]{
            "Mayor numero en las caras"
        });

        //Propiedades de Pieces_Sorters
        Pieces_Sorters = new Label();
        Pieces_Sorters.Location = new Point(350,80);
        Pieces_Sorters.Size = new Size(250,50);
        Pieces_Sorters.Name = "Pieces_Sorters";
        Pieces_Sorters.Text = "Ordenar fichas por";
        Pieces_Sorters.Visible = true;
        Pieces_Sorters.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de SorterPiecesByPlayer
        SorterPiecesByPlayer = new ComboBox();
        SorterPiecesByPlayer.Location = new Point(350,200);
        SorterPiecesByPlayer.Size = new Size(190,20);
        SorterPiecesByPlayer.Name = "SorterPiecesByPlayer";
        SorterPiecesByPlayer.Text = "Modo clasico de domino";
        SorterPiecesByPlayer.FormattingEnabled = true;
        SorterPiecesByPlayer.Items.AddRange(new object[]{
            "Modo clasico de domino"
        });

        //Propiedades de Sorter_PiecesByPlayer
        Sorter_PiecesByPlayer = new Label();
        Sorter_PiecesByPlayer.Location = new Point(350,140);
        Sorter_PiecesByPlayer.Size = new Size(250,70);
        Sorter_PiecesByPlayer.Name = "Sorter_PiecesByPlayer";
        Sorter_PiecesByPlayer.Text = "Modo de repartir fichas";
        Sorter_PiecesByPlayer.Visible = true;
        Sorter_PiecesByPlayer.Font = new Font(this.Font.FontFamily,18f,FontStyle.Italic);

        //Propiedades De UpdatersMode
        UpdatersMode = new ComboBox();
        UpdatersMode.Location = new Point(350,280);
        UpdatersMode.Size = new Size(190,20);
        UpdatersMode.Name = "UpdatersModes";
        UpdatersMode.Text = "Modo clasico de domino";
        UpdatersMode.FormattingEnabled = true;
        UpdatersMode.Items.AddRange(new object[]{
            "Modo clasico de domino"
        });

        //Propiedades de Updaters_Mode
        Updaters_Mode = new Label();
        Updaters_Mode.Location = new Point(350,240);
        Updaters_Mode.Size = new Size(250,50);
        Updaters_Mode.Name = "Updaters_Mode";
        Updaters_Mode.Text = "Modo de juego";
        Updaters_Mode.Visible = true;
        Updaters_Mode.Font = new Font(this.Font.FontFamily,20f,FontStyle.Italic);

        //Propiedades de WinnerComputerMode
        WinnerComputerMode = new ComboBox();
        WinnerComputerMode.Location = new Point(350,360);
        WinnerComputerMode.Size = new Size(190,20);
        WinnerComputerMode.Name = "WinnerComputerMode";
        WinnerComputerMode.Text = "Modo clasico de domino";
        WinnerComputerMode.FormattingEnabled = true;
        WinnerComputerMode.Items.AddRange(new object[]{
            "Modo clasico de domino",
            "Menos pasado",
            "Menos fichas en mano"
        });

        //Propiedades de Winner_ComputerMode
        Winner_ComputerMode = new Label();
        Winner_ComputerMode.Location = new Point(350,300);
        Winner_ComputerMode.Size = new Size(250,70);
        Winner_ComputerMode.Name = "Winner_ComputerMode";
        Winner_ComputerMode.Text = "Determinar el ganador por";
        Winner_ComputerMode.Visible = true;
        Winner_ComputerMode.Font = new Font(this.Font.FontFamily,18f,FontStyle.Italic);

        //Propiedades de GameInfo
        GameInfo = new TextBox();
        GameInfo.Location = new Point(1050,0);
        GameInfo.Size = new Size(300,200);
        GameInfo.Multiline = true;
        GameInfo.Enabled = false;
        GameInfo.Name = "GameInfo";
        GameInfo.Font = new Font(this.Font.FontFamily,15f,FontStyle.Italic);

        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1500, 700);
        this.Text = "Form1";
        Controls.Add(StartButton);
        Controls.Add(PiecesTypes);
        Controls.Add(PiecesInfo);
        Controls.Add(TypePieces);
        Controls.Add(PlayersCreator);
        Controls.Add(Players);
        Controls.Add(PiecesByPlayer);
        Controls.Add(NumberOfPiecesByPlayer);
        Controls.Add(MaxNumberOfPiecesFaces);
        Controls.Add(Max_NumberOfPiecesFaces);
        Controls.Add(CompatibilityControlers);
        Controls.Add(CompatibilityPieces);
        Controls.Add(StartersGames);
        Controls.Add(Starters_Games);
        Controls.Add(GamesComparers);
        Controls.Add(ComparersGames);
        Controls.Add(StartersGamesModes);
        Controls.Add(SelecterGamesStarters);
        Controls.Add(GameOversModes);
        Controls.Add(OversModes);
        Controls.Add(NextPlayer);
        Controls.Add(NextPlayerMode);
        Controls.Add(Soters_Pieces);
        Controls.Add(Pieces_Sorters);
        Controls.Add(SorterPiecesByPlayer);
        Controls.Add(Sorter_PiecesByPlayer);
        Controls.Add(UpdatersMode);
        Controls.Add(Updaters_Mode);
        Controls.Add(WinnerComputerMode);
        Controls.Add(Winner_ComputerMode);

        ((System.ComponentModel.ISupportInitialize)BoxPainter).EndInit();
        ResumeLayout();
    }
    private PictureBox BoxPainter;
    private Button StepButton;
    private Button StartButton;
    private Button GameStartButton;
    private ComboBox PiecesTypes;
    private TextBox PiecesInfo;
    private Label TypePieces;
    private Button PlayersCreator;
    private Label Players;
    private TextBox PiecesByPlayer;
    private Label NumberOfPiecesByPlayer;
    private Label MaxNumberOfPiecesFaces;
    private TextBox Max_NumberOfPiecesFaces;
    private ComboBox CompatibilityControlers;
    private Label CompatibilityPieces;
    private ComboBox StartersGames;
    private Label Starters_Games;
    private ComboBox GamesComparers;
    private Label ComparersGames;
    private ComboBox StartersGamesModes;
    private Label SelecterGamesStarters;
    private ComboBox GameOversModes;
    private Label OversModes;
    private ComboBox NextPlayer;
    private Label NextPlayerMode;
    private ComboBox Soters_Pieces;
    private Label Pieces_Sorters;
    private ComboBox SorterPiecesByPlayer;
    private Label Sorter_PiecesByPlayer;
    private TextBox GameInfo;
    private ComboBox UpdatersMode;
    private Label Updaters_Mode;
    private ComboBox WinnerComputerMode;
    private Label Winner_ComputerMode;

    #endregion
}
