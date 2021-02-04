using Godot;

public class HUDController : Panel
{   
    [Export]
    public PackedScene TitlePage;

    [Export]
    public PackedScene GamePage;
    enum Screens{
        Title,
        Game,
        Instructions,
        Credits
    }

    private MarginContainer _margin;

    private Screens _ActiveScreenState;
    private Node _ActiveScreen;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _margin = GetNode<MarginContainer>("MarginContainer");
        TitlePage = GD.Load<PackedScene>("res://Scenes/TitlePage.tscn");
        GamePage = GD.Load<PackedScene>("res://Scenes/GamePage.tscn");
        _ActiveScreenState = Screens.Title;
        DisplayScreen();
    }

    private void DisplayScreen()
    {
        switch (_ActiveScreenState)
        {
            case Screens.Title:
                _margin.AddChild(TitlePage.Instance());
                GetNode("MarginContainer/TitlePage").Connect("PlayPressed", this, nameof(OnPlayPressed));
                break;
            case Screens.Game:
                _margin.AddChild(GamePage.Instance());
                GetNode("MarginContainer/GamePage").Connect("QuitToMainMenu", this, nameof(OnQuitToMainMenu));
                break;
            case Screens.Instructions:
                break;
            case Screens.Credits:
                break;
        }
    }

    private void OnPlayPressed()
    {
        _margin.RemoveChild(GetNode("MarginContainer/TitlePage"));
        _ActiveScreenState = Screens.Game;
        DisplayScreen();
    }

    private void OnQuitToMainMenu()
    {
        _margin.RemoveChild(GetNode("MarginContainer/GamePage"));
        _ActiveScreenState = Screens.Title;
        DisplayScreen();  
    }
}
