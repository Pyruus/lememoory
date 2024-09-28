using Godot;
using System;

public partial class MainMenu : Control
{
	private Button startButton;
	private Button quitButton;
	
	private PackedScene questionMenuScene;
	private QuestionMenu questionMenu;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton = GetNode<Button>("VBoxContainer/StartButton");
		quitButton = GetNode<Button>("VBoxContainer/QuitButton");

		startButton.Pressed += OnStartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;
		
		/*questionMenuScene = ResourceLoader.Load<PackedScene>("res://Scenes/question_menu.tscn");
		questionMenu = questionMenuScene.Instantiate<QuestionMenu>();*/
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/board.tscn");
		/*AddChild(questionMenu);
		
		questionMenu.ShowRandomQuestion();*/
	}
	
	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
