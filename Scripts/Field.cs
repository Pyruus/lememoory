using Godot;
using System;
using System.Collections.Generic;
public partial class Field : Area2D
{
	public enum TileType { QUESTION, NORMAL, SPECIAL, PENALTY }
	public TileType tileType = TileType.NORMAL;
	
	public List<Field> neighbours = new List<Field>();
	public Field previouslyVisitedField = null;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		setTileTexture();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void setTileTexture() {
		var sprite = GetNode<Sprite2D>("Sprite2D");

		
		switch (tileType)
		{
			case TileType.QUESTION:
				sprite.Texture = (Texture2D)GD.Load("res://textures/start_tile.png");
				break;
			case TileType.NORMAL:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_1.png");
				break;
			case TileType.SPECIAL:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_magia.png");
				break;
			case TileType.PENALTY:
				sprite.Texture = (Texture2D)GD.Load("res://textures/penalty_tile.png");
				break;
		}
		
		float scaleX = 128.0f / sprite.Texture.GetWidth();
		float scaleY = 128.0f / sprite.Texture.GetHeight();
		sprite.Scale = new Vector2(scaleX, scaleY);
	}
}
