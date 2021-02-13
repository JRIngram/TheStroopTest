using Godot;
using System;

public class CreditsPage : VBoxContainer
{
    [Signal]
    public delegate void QuitToMainMenuPressed();
    public override void _Ready()
    {
        GetNode<Button>("QuitToMainMenu").Connect("pressed", this, nameof(OnQuitToMainMenuPressed));
    }

    private void OnQuitToMainMenuPressed()
    {
        EmitSignal(nameof(QuitToMainMenuPressed));
    }
}
