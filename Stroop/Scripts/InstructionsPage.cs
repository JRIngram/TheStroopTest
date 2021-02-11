using Godot;
using System;

public class InstructionsPage : VBoxContainer
{
    [Signal]
    public delegate void PlayPressed();

    [Signal]
    public delegate void QuitToMainMenuPressed();
    public override void _Ready()
    {
        GetNode<Button>("ButtonContainer/Play").Connect("pressed", this, nameof(OnPlayPressed));
        GetNode<Button>("ButtonContainer/Quit").Connect("pressed", this, nameof(OnQuitToMainMenuPressed));
    }

    private void OnPlayPressed()
    {
        EmitSignal(nameof(PlayPressed));
    }

    private void OnQuitToMainMenuPressed()
    {
        EmitSignal(nameof(QuitToMainMenuPressed));
    }
}
