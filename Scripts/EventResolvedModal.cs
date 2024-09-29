using Godot;
using System;

public partial class EventResolvedModal : Node2D
{
	public Label Title { get; set; }
	public Label Description { get; set; }
	private Button closeButton;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Title = GetNode<Label>("VBoxContainer/Title");
		Description = GetNode<Label>("VBoxContainer/Description");
		
		closeButton = GetNode<Button>("VBoxContainer/CloseButton");
		closeButton.Pressed += OnCloseButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnCloseButtonPressed()
	{
		Hide();
	}
}
