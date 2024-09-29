using Godot;
using System;

public partial class EventResolvedModal : Node2D
{
	public Label Title { get; set; }
	public Label Description { get; set; }
	private Button closeButton;

	private Panel panel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Title = GetNode<Label>("Panel/VBoxContainer/Title");
		Description = GetNode<Label>("Panel/VBoxContainer/Description");
		
		closeButton = GetNode<Button>("Panel/VBoxContainer/CloseButton");
		closeButton.Pressed += OnCloseButtonPressed;

		panel = GetNode<Panel>("Panel");
		panel.Size = new Vector2(GetViewport().GetVisibleRect().Size.X, GetViewport().GetVisibleRect().Size.Y);
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
