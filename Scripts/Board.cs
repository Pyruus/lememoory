using Godot;
using System;
using System.Collections.Generic;

public partial class Board : Node2D
{
	private List<Field> tiles = new List<Field>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CreateBoard();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	   
	private void CreateBoard()
	{
	   var outerFields = new Field.TileType[]
		{
			Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
			Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
			Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
			Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL
		};
		var fieldSizePixels = 64;
		var fieldsInRow = 4;
		var index = 0;
		 foreach (var item in outerFields)
		{
		var currentField = (Field)GD.Load<PackedScene>("res://scenes/Field.tscn").Instantiate();
		currentField.tileType = item;

		Vector2 position;

		if (index < fieldsInRow)
		{
			position = new Vector2(index * fieldSizePixels, 0);
		}
		else if (index < fieldsInRow * 2)
		{
			position = new Vector2((fieldsInRow - 1) * fieldSizePixels, (index - fieldsInRow) * fieldSizePixels);
		}
		else if (index < fieldsInRow * 3)
		{
			position = new Vector2((fieldsInRow * 3 - 1 - index) * fieldSizePixels, (fieldsInRow - 1) * fieldSizePixels);
		}
		else
		{
			position = new Vector2(0, (fieldsInRow * 4 - 1 - index) * fieldSizePixels);
		}

		currentField.Position = position;
		AddChild(currentField);
		index++;
	}
	}
}
