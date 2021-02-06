using Godot;
using System;

public class TitlePage : VBoxContainer
{

    [Signal]
    public delegate void PlayPressed();

    public override void _Ready()
    {
        GetNode<AudioStreamPlayer>("Theme").Play();
    }

    private void OnPlayPressed()
    {
        EmitSignal(nameof(PlayPressed));
    }

    private void OnQuitPressed()
    {
        GetTree().Quit();     
    }
}
