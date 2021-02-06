using Godot;
using System;

public class GameLabels : HBoxContainer
{
    private Label _scoreLabel;
    private Label _timeLabel;

    public override void _Ready()
    {
        _scoreLabel = GetNode<Label>("Score");
        _scoreLabel.Text = "";
        _timeLabel = GetNode<Label>("TimeRemaining");
        _timeLabel.Text = "";
        VBoxContainer gameManager = GetNode<VBoxContainer>("../../GamePage");
        gameManager.Connect("ScoreUpdated", this, nameof(OnScoreUpdated));
        gameManager.Connect("TimeRemainingUpdated", this, nameof(OnTimeRemainingUpdated));
        gameManager.Connect("StartGame", this, nameof(OnStartGame));
        gameManager.Connect("GameOver", this, nameof(OnGameOver));
    }

    private void OnStartGame()
    {
        _scoreLabel.Text= "Score: 0";
        _timeLabel.Text = "All time remaining!";
    }

    private void OnGameOver()
    {
        _timeLabel.Text = "";
        _timeLabel.QueueFree();
    }

    private void OnScoreUpdated(int score)
    {
        _scoreLabel.Text = "Score: " + score;
    }

    private void OnTimeRemainingUpdated(int timeRemaining)
    {
        _timeLabel.Text = timeRemaining + " seconds remaining!";
    }
}
