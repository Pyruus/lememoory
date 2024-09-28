using Godot;
using System;

public partial class MainMenu : Control
{
	private Button startButton;
	private Button quitButton;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton = GetNode<Button>("VBoxContainer/StartButton");
		quitButton = GetNode<Button>("VBoxContainer/QuitButton");

		startButton.Pressed += OnStartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/board.tscn");
	}
	
	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
