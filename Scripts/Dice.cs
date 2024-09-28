using Godot;
using System;

public partial class Dice : Control
{
	private Sprite2D sprite;
	private readonly Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite = GetNode<Sprite2D>("VBoxContainer/Sprite2D");
		sprite.Texture = (Texture2D)GD.Load("res://Assets/kosc1.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public int OnRollButtonPressed()
	{
		var result = random.Next(1, 7);
		sprite.Texture = result switch
		{
			1 => (Texture2D)GD.Load("res://Assets/kosc1.png"),
			2 => (Texture2D)GD.Load("res://Assets/kosc2.png"),
			3 => (Texture2D)GD.Load("res://Assets/kosc3.png"),
			4 => (Texture2D)GD.Load("res://Assets/kosc4.png"),
			5 => (Texture2D)GD.Load("res://Assets/kosc5.png"),
			6 => (Texture2D)GD.Load("res://Assets/kosc6.png"),
			_ => (Texture2D)GD.Load("res://Assets/kosc1.png"),
		};
		return result;
	}
	
}
