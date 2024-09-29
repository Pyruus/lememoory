using Godot;
using System;

public partial class MainMenu : Control
{
	private Button startButton;
	private MenuButton categoriesList;
	private Button quitButton;
	private VBoxContainer vBoxContainer;
	
	private PackedScene questionMenuScene;
	private QuestionMenu questionMenu;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startButton = GetNode<Button>("VBoxContainer/StartButton");
		categoriesList = GetNode<MenuButton>("VBoxContainer/CategoriesList");
		quitButton = GetNode<Button>("VBoxContainer/QuitButton");
		vBoxContainer = GetNode<VBoxContainer>("VBoxContainer");

		startButton.Pressed += OnStartButtonPressed;
		quitButton.Pressed += OnQuitButtonPressed;

		categoriesList.Text = Globals.SelectedQuestionsCategory.name;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		categoriesList.Text = Globals.SelectedQuestionsCategory.name;
		
		vBoxContainer.Size = new Vector2(GetViewport().GetVisibleRect().Size.X, 720);
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
