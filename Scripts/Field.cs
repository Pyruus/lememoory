using Godot;
using System;

public partial class Field : Area2D
{
	public enum TileType { QUESTION, NORMAL, BONUS, PENALTY }
	public TileType tileType = TileType.NORMAL;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void setTileTexture() {
		var sprite = GetNode<Sprite2D>("Sprite");

		switch (tileType)
		{
			case TileType.QUESTION:
				sprite.Texture = (Texture2D)GD.Load("res://textures/start_tile.png");
				break;
			case TileType.NORMAL:
				sprite.Texture = (Texture2D)GD.Load("res://textures/normal_tile.png");
				break;
			case TileType.BONUS:
				sprite.Texture = (Texture2D)GD.Load("res://textures/bonus_tile.png");
				break;
			case TileType.PENALTY:
				sprite.Texture = (Texture2D)GD.Load("res://textures/penalty_tile.png");
				break;
		}
	}
}
