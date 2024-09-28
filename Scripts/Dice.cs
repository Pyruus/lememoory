using Godot;
using System;

public partial class Dice : Control
{
	private Label resultLabel;
	private Button rollButton;
	
	public int LatestResult;
	
	private readonly Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		resultLabel = GetNode<Label>("VBoxContainer/ResultLabel");
		rollButton = GetNode<Button>("VBoxContainer/RollButton");
		
		rollButton.Pressed += OnRollButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnRollButtonPressed()
	{
		var result = random.Next(1, 7);
		resultLabel.Text = result.ToString();
		LatestResult = result;
	}
	
}
