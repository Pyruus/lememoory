using Godot;
using System;

public partial class WinnerScreen : Node2D
{
	public Label label;

	private Panel panel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		label = GetNode<Label>("Panel/VBoxContainer/Label");
		panel = GetNode<Panel>("Panel");
		
		panel.Size = new Vector2(GetViewport().GetVisibleRect().Size.X, GetViewport().GetVisibleRect().Size.Y);
		panel.ZIndex = 1000;

		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
