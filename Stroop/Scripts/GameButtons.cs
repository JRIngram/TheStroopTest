using Godot;
using System;

public class GameButtons : VBoxContainer
{

    [Signal]
    public delegate void ColorButtonPressed(String color);

    [Signal]
    public delegate void QuitButtonPressed();

    private Button _redButton;
    private Button _blueButton;
    private Button _yellowButton;

    public override void _Ready()
    {
        //Set up Buttons to be initially disabled
        _redButton = GetNode<Button>("ColorButtons/RedButton");
        _blueButton = GetNode<Button>("ColorButtons/BlueButton");
        _yellowButton = GetNode<Button>("ColorButtons/YellowButton");
        _redButton.Connect("pressed", this, nameof(OnRedButtonPressed));
        _blueButton.Connect("pressed", this, nameof(OnBlueButtonPressed));
        _yellowButton.Connect("pressed", this, nameof(OnYellowButtonPressed));
        _redButton.Disabled = true;
        _blueButton.Disabled = true;
        _yellowButton.Disabled = true;
        
        Button quitButton = GetNode<Button>("QuitToMainMenu");
        quitButton.Connect("pressed", this, nameof(OnQuitToMainMenuPressed));

        VBoxContainer gameManager = GetNode<VBoxContainer>("../../GamePage");
        gameManager.Connect("StartGame", this, nameof(OnStartGame));
        gameManager.Connect("GameOver", this, nameof(OnGameOver));
    }

    private void OnStartGame()
    {
        _redButton.Disabled = false;
        _blueButton.Disabled = false;
        _yellowButton.Disabled = false; 
    }

    private void OnRedButtonPressed(){
        EmitSignal(nameof(ColorButtonPressed), "red");
    }

    private void OnBlueButtonPressed(){
        EmitSignal(nameof(ColorButtonPressed), "blue");
    }

    private void OnYellowButtonPressed(){
        EmitSignal(nameof(ColorButtonPressed), "yellow");
    }

    private void OnQuitToMainMenuPressed(){
        EmitSignal(nameof(QuitButtonPressed));
    }

    private void OnGameOver()
    {
        _redButton.Disabled = true;
        _blueButton.Disabled = true;
        _yellowButton.Disabled = true;
    }
}
