using Godot;

public class HUDController : Panel
{   
    [Export]
    public PackedScene TitlePage;

    [Export]
    public PackedScene GamePage;

    [Export]
    public PackedScene InstructionsPage;

    [Export]
    public PackedScene CreditsPage;

    enum Screens{
        Title,
        Game,
        Instructions,
        Credits
    }

    private MarginContainer _margin;

    private Screens _ActiveScreen;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _margin = GetNode<MarginContainer>("MarginContainer");
        TitlePage = GD.Load<PackedScene>("res://Scenes/TitlePage.tscn");
        GamePage = GD.Load<PackedScene>("res://Scenes/GamePage.tscn");
        InstructionsPage = GD.Load<PackedScene>("res://Scenes/InstructionsPage.tscn");
        CreditsPage = GD.Load<PackedScene>("res://Scenes/CreditsPage.tscn");
        _ActiveScreen = Screens.Title;
        DisplayScreen();
    }


    /// <summary> 
    ///     Removes the current screen and loads a new screen based on the passed screenToLoad parameter
    /// </summary>
    /// <param name="screenToLoad">The screen the function loads after removing the current screen </param>
    private void ChangeScreen(Screens screenToLoad)
    {
        RemoveCurrentScreen();
        _ActiveScreen = screenToLoad;
        DisplayScreen();
    }

    /// <summary> Removes a screen based off of the value of _ActiveScreenState </summary>
    private void RemoveCurrentScreen()
    {
        switch (_ActiveScreen)
        {
            case Screens.Title:
                _margin.RemoveChild(GetNode("MarginContainer/TitlePage"));
                break;
            case Screens.Game:
                _margin.RemoveChild(GetNode("MarginContainer/GamePage"));
                break;
            case Screens.Instructions:
                _margin.RemoveChild(GetNode("MarginContainer/InstructionsPage"));
                break;
            case Screens.Credits:
                _margin.RemoveChild(GetNode("MarginContainer/CreditsPage"));
                break;

        }
    }

    /// <summary> Loads a new screen based off of the value of _ActiveScreenState </summary>
    private void DisplayScreen()
    {
        switch (_ActiveScreen)
        {
            case Screens.Title:
                _margin.AddChild(TitlePage.Instance());
                GetNode("MarginContainer/TitlePage").Connect("PlayPressed", this, nameof(OnPlayPressed));
                GetNode("MarginContainer/TitlePage").Connect("InstructionsPressed", this, nameof(OnInstructionsPressed));
                GetNode("MarginContainer/TitlePage").Connect("CreditsPressed", this, nameof(OnCreditsPressed));
                break;
            case Screens.Game:
                _margin.AddChild(GamePage.Instance());
                GetNode("MarginContainer/GamePage").Connect("QuitToMainMenu", this, nameof(OnQuitToMainMenu));
                break;
            case Screens.Instructions:
                _margin.AddChild(InstructionsPage.Instance());
                GetNode("MarginContainer/InstructionsPage").Connect("PlayPressed", this, nameof(OnPlayPressed));
                GetNode("MarginContainer/InstructionsPage").Connect("QuitToMainMenuPressed", this, nameof(OnQuitToMainMenu));
                break;
            case Screens.Credits:
                _margin.AddChild(CreditsPage.Instance());
                GetNode("MarginContainer/CreditsPage").Connect("QuitToMainMenuPressed", this, nameof(OnQuitToMainMenu));
                break;
        }
    }
    private void OnPlayPressed()
    {
        ChangeScreen(Screens.Game);
    }

    private void OnInstructionsPressed()
    {
        ChangeScreen(Screens.Instructions);
    }

    private void OnCreditsPressed()
    {
        ChangeScreen(Screens.Credits);
    }

    private void OnQuitToMainMenu()
    {
        ChangeScreen(Screens.Title);
    }
}
