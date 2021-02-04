using Godot;
using System;
using System.Collections.Generic;

public class GameManager : VBoxContainer
{
    [Signal]
    public delegate void QuitToMainMenu();
    private Color _CurrentColor;
    private Label _wordLabel;
    private int _score;
    private Dictionary<String, Color> Colors;

    private Label scoreLabel;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Colors = new Dictionary<string, Color>();
        Color red = new Color(.722f, .116f, .116f);
        Color blue = new Color(.212f, .212f, .833f);
        Color yellow = new Color(.884f, .884f, .27f);
        Colors.Add("red", red);
        Colors.Add("blue", blue);
        Colors.Add("yellow", yellow);

        _wordLabel = GetNode<Label>("WordContainer/Word");
        chooseCurrentWord();
        scoreLabel = GetNode<Label>("Labels/Score");
        _score = 0;
        scoreLabel.Text = "Score: " + _score;
    }

    public void chooseCurrentWord()
    {
        String word = selectWord();
        _CurrentColor = selectColor();
        _wordLabel.Text = word;
        _wordLabel.Set("custom_colors/font_color", _CurrentColor);
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

    private void checkCorrectButtonPressed(Color buttonColor){
        if(buttonColor == _CurrentColor){
            _score++;
            scoreLabel.Text = "Score: " + _score;
            AudioStreamPlayer correctAnswer = GetNode<AudioStreamPlayer>("CorrectAnswer");
            correctAnswer.Play();
        }
        else{
            AudioStreamPlayer WrongAnswer = GetNode<AudioStreamPlayer>("WrongAnswer");
            WrongAnswer.Play();
        }
        chooseCurrentWord();
    }

    private void _OnRedButtonButtonDown(){
        checkCorrectButtonPressed(Colors["red"]);
    }

    private void _OnBlueButtonButtonDown(){
        checkCorrectButtonPressed(Colors["blue"]);
    }

    private void _OnYellowButtonButtonDown(){
        checkCorrectButtonPressed(Colors["yellow"]);
    }

    private void _OnQuitToMainMenuPressed(){
        EmitSignal(nameof(QuitToMainMenu));
    }
}

