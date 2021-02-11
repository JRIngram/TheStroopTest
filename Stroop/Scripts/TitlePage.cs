using Godot;
using System;

public class TitlePage : VBoxContainer
{

    [Signal]
    public delegate void PlayPressed();

    [Signal]
    public delegate void InstructionsPressed();

    public override void _Ready()
    {
        //GetNode<AudioStreamPlayer>("Theme").Play();
        GetNode<Button>("Buttons/Play").Connect("pressed", this, nameof(OnPlayPressed));
        GetNode<Button>("Buttons/Instructions").Connect("pressed", this, nameof(OnInstructionsPressed));
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

    private void OnQuitPressed()
    {
        GetTree().Quit();     
    }
}
