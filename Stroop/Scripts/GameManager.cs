using Godot;
using System;

public class GameManager : VBoxContainer
{
    [Signal]
    public delegate void ColorChosen(String buttonColor);

    [Signal]
    public delegate void ScoreUpdated(int score);

    [Signal]
    public delegate void StartTimerUpdated(int timeBeforeStart);

    [Signal]
    public delegate void TimeRemainingUpdated(int timeRemaining);

    [Signal]
    public delegate void StartGame();

    [Signal]
    public delegate void GameOver();

    [Signal]
    public delegate void QuitToMainMenu();

    private int _score;
    
    [Export]
    private int _secondsRemaining;


    private Timer _timeRemaining;

    private Timer _startTimer;

    private int _timeBeforeStart;

    private bool _gameStarted;

    public override void _Ready()
    {
        CenterContainer word = GetNode<CenterContainer>("WordContainer");
        word.Connect("ColorValidationComplete", this, nameof(_OnColorValidationComplete));

        //Connect Color and Quit Buttons
        VBoxContainer gameButtons = GetNode<VBoxContainer>("GameButtons");
        gameButtons.Connect("ColorButtonPressed", this, nameof(CheckCorrectButtonPressed));
        gameButtons.Connect("QuitButtonPressed", this, nameof(OnQuitToMainMenuPressed));

        _startTimer = GetNode<Timer>("StartTimer");
        _startTimer.Connect("timeout", this, nameof(OnStartTimerTimeout));
        _timeBeforeStart = 3;
        _gameStarted = false;

        _timeRemaining = GetNode<Timer>("TimeRemaining");
        _timeRemaining.Connect("timeout", this, nameof(OnTimeRemainingTimeout));
    }

    private void OnStartTimerTimeout()
    {
        if(_gameStarted == false)
        {
            _timeBeforeStart--;
            if(_timeBeforeStart == 0)
            {
                _gameStarted = true;
                EmitSignal(nameof(StartGame));
                _startTimer.Stop();
                _timeRemaining.Start(1);
                _startTimer.QueueFree();
            }
            EmitSignal(nameof(StartTimerUpdated), _timeBeforeStart);
        }
    }

    private void OnTimeRemainingTimeout()
    {
        if(_gameStarted)
        {
            if(_secondsRemaining > 0){
                _secondsRemaining--;
                EmitSignal(nameof(TimeRemainingUpdated), _secondsRemaining);
            }
            if(_secondsRemaining == 0){
                EmitSignal(nameof(GameOver));
                _timeRemaining.Stop();
                _timeRemaining.QueueFree();
            }
        }
    }

    private void CheckCorrectButtonPressed(String buttonColor)
    {
        EmitSignal(nameof(ColorChosen), buttonColor);
    }

    private void _OnColorValidationComplete(bool correct)
    {
        if(correct){
            _score++;
            EmitSignal(nameof(ScoreUpdated), _score);
            AudioStreamPlayer correctAnswer = GetNode<AudioStreamPlayer>("CorrectAnswer");
            correctAnswer.Play();
        }
        else{
            AudioStreamPlayer wrongAnswer = GetNode<AudioStreamPlayer>("WrongAnswer");
            wrongAnswer.Play();
        }
        System.Diagnostics.Debug.WriteLine("Score: " + _score);
    }

    private void OnQuitToMainMenuPressed()
    {
        EmitSignal(nameof(QuitToMainMenu));
    }
}

