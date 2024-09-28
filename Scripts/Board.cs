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
			Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
				Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
				Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
				Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
				Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
					Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,
				


		};
		var fieldSizePixels = 128;
		var fieldsInRow = 13;
		var fieldsInColumn = 7;
		var index = 0;
		Field previousField = null;
		 foreach (var item in outerFields)
		{
		var currentField = (Field)GD.Load<PackedScene>("res://scenes/Field.tscn").Instantiate();
		currentField.tileType = item;

		Vector2 position;

		if (index < fieldsInRow)
		{
			position = new Vector2(index * fieldSizePixels, 0);
		}
		else if (index < fieldsInRow + fieldsInColumn - 1)
		{
			position = new Vector2((fieldsInRow - 1) * fieldSizePixels, (index - fieldsInRow + 1) * fieldSizePixels);
		}
		else if (index < fieldsInRow * 2 + fieldsInColumn -2)
		{
			position = new Vector2(( fieldsInRow * 2 +  fieldsInColumn -3 - index) * fieldSizePixels, (fieldsInColumn - 1) * fieldSizePixels );
		}
		else
		{
			position = new Vector2(0, (fieldsInRow * 2 + fieldsInColumn +3 - index) * fieldSizePixels);
		}

		currentField.Position = position;
		currentField.previousField = previousField;
		AddChild(currentField);
		index++;
		if(previousField != null) {
			previousField.nextField = currentField;
		}
		previousField = currentField;	
		GD.Print(currentField.previousField);
		
	}
	
		tiles[0].previousField = previousField;
	
	
	
		int[,] specialPathsIndexPositions = {
			{2, 1}, {1,2}, {fieldsInRow - 3, 1}, {fieldsInRow -2, 2 }, {1, fieldsInColumn - 3}, {2, fieldsInColumn-2}, {fieldsInRow - 3, fieldsInColumn - 2}, {fieldsInRow -2, fieldsInColumn -3},
		 {(int)Math.Floor(fieldsInRow/2f), 1},
		 {(int)Math.Floor(fieldsInRow/2f), 2},
		 {(int)Math.Floor(fieldsInRow/2f), 4},
		 {(int)Math.Floor(fieldsInRow/2f), 5},
		};
		
		for(int i = 0; i< 12; i++){
		 
			var currentSpecialField = (Field)GD.Load<PackedScene>("res://scenes/Field.tscn").Instantiate();
		currentSpecialField.tileType = Field.TileType.NORMAL;
		currentSpecialField.Position = new Vector2(specialPathsIndexPositions[i, 0] * fieldSizePixels, specialPathsIndexPositions[i, 1] * fieldSizePixels);
		AddChild(currentSpecialField);
		}
		
		int[,] specialFieldsIndexes = {
			{2, 2}, {fieldsInRow - 3, 2}, {2, fieldsInColumn - 3},  {fieldsInRow -3, fieldsInColumn -3},
		};
				
		for(int i = 0; i< 4; i++){
			var currentSpecialFieldMain = (Field)GD.Load<PackedScene>("res://scenes/Field.tscn").Instantiate();
			currentSpecialFieldMain.tileType = Field.TileType.SPECIAL;
			currentSpecialFieldMain.Position = new Vector2(specialFieldsIndexes[i, 0] * fieldSizePixels, specialFieldsIndexes[i, 1] * fieldSizePixels);
			AddChild(currentSpecialFieldMain);
		}
		
		
	}
}
