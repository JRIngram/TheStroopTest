using Godot;
using System;

public class TitlePage : VBoxContainer
{

    [Signal]
    public delegate void PlayPressed();

    [Signal]
    public delegate void InstructionsPressed();

    [Signal]
    public delegate void CreditsPressed();

    public override void _Ready()
    {
        GetNode<AudioStreamPlayer>("Theme").Play();
        GetNode<Button>("Buttons/Play").Connect("pressed", this, nameof(OnPlayPressed));
        GetNode<Button>("Buttons/Instructions").Connect("pressed", this, nameof(OnInstructionsPressed));
        GetNode<Button>("Buttons/Credits").Connect("pressed", this, nameof(OnCreditsPressed));
        GetNode<Button>("Buttons/Quit").Connect("pressed", this, nameof(OnQuitPressed));
    }

    private void OnPlayPressed()
    {
        EmitSignal(nameof(PlayPressed));
    }

    private void OnInstructionsPressed()
    {
        EmitSignal(nameof(InstructionsPressed));
    }

    private void OnCreditsPressed()
    {
        EmitSignal(nameof(CreditsPressed));
    }

    private void OnQuitPressed()
    {
        GetTree().Quit();     
    }
}
