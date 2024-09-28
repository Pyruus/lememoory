using Godot;
using System;

public partial class MainMenu : Control
{
	private Button startButton;
	private MenuButton categoriesList;
	private Button quitButton;
	
	private PackedScene questionMenuScene;
	private QuestionMenu questionMenu;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton = GetNode<Button>("VBoxContainer/StartButton");
		categoriesList = GetNode<MenuButton>("VBoxContainer/CategoriesList");
		quitButton = GetNode<Button>("VBoxContainer/QuitButton");

		startButton.Pressed += OnStartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;

		categoriesList.Text = Globals.SelectedQuestionsCategory.name;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		categoriesList.Text = Globals.SelectedQuestionsCategory.name;
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
