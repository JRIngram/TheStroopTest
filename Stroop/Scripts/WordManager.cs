using Godot;
using System;
using System.Collections.Generic;

public class WordManager : CenterContainer
{
    [Signal]
    public delegate void ColorValidationComplete(bool correctColor);
    private Dictionary<String, Color> Colors;
    private Color _currentColor;
    private Label _wordLabel;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Colors = new Dictionary<string, Color>();
        Color red = new Color(.722f, .116f, .116f);
        Color blue = new Color(.212f, .212f, .833f);
        Color yellow = new Color(.884f, .884f, .27f);
        Color white = new Color(1f,1f,1f);
        Colors.Add("red", red);
        Colors.Add("blue", blue);
        Colors.Add("yellow", yellow);
        Colors.Add("white", white);

        _wordLabel = GetNode<Label>("Word");
        _wordLabel.Set("custom_colors/font_color", Colors["red"]);
        VBoxContainer gameManager = GetNode<VBoxContainer>("../../GamePage");
        gameManager.Connect("ColorChosen", this, nameof(OnColorChosen));
        gameManager.Connect("StartTimerUpdated", this, nameof(OnStartTimerUpdated));
        gameManager.Connect("StartGame", this, nameof(OnStartGame));
        gameManager.Connect("GameOver", this, nameof(OnGameOver));
    }

    private void OnStartTimerUpdated(int timeBeforeStart)
    {
        switch(timeBeforeStart){
            case 3:
                _wordLabel.Text = "Ready";
                _wordLabel.Set("custom_colors/font_color", Colors["red"]);
                break;
            case 2: 
                _wordLabel.Text = "Set";
                _wordLabel.Set("custom_colors/font_color", Colors["yellow"]);
                break;
            case 1:
                _wordLabel.Text = "Go!";
                _wordLabel.Set("custom_colors/font_color", Colors["blue"]);
                break;
        }
    }

    private void OnStartGame()
    {
        chooseCurrentWordAndColor();
    }

    private void OnGameOver()
    {
        _wordLabel.Text = "Game Over!";
        _wordLabel.Set("custom_colors/font_color", Colors["white"]);
    }

    private void OnColorChosen(String buttonColor)
    {
        bool correctColorChosen = Colors[buttonColor] == _currentColor;
        chooseCurrentWordAndColor();
        EmitSignal(nameof(ColorValidationComplete), correctColorChosen);
    }

    public Color getCurrentColor(){
        return _currentColor;
    }

    public void chooseCurrentWordAndColor()
    {
        String word = selectWord();
        _currentColor = selectColor();
        _wordLabel.Text = word;
        _wordLabel.Set("custom_colors/font_color", _currentColor);
    }

    private String selectWord()
    {
        String[] availableWords = {"red", "blue", "yellow"};
        Random random = new Random();
        String selectedWord = availableWords[random.Next(0,3)];
        return selectedWord;
    }

    private Color selectColor()
    {
        Color[] availableColors = {Colors["red"], Colors["blue"], Colors["yellow"]};
        Random random = new Random();
        Color selectedColor = availableColors[random.Next(0,3)];
        return selectedColor;
    }
}
