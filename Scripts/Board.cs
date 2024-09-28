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

	private void CreateBoard()
	{
		var boardLayout = new Field.TileType?[,] {
			{ Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL },
			{ Field.TileType.NORMAL, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, Field.TileType.NORMAL },
			{ Field.TileType.NORMAL,  Field.TileType.NORMAL, Field.TileType.SPECIAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.SPECIAL, Field.TileType.NORMAL, Field.TileType.NORMAL },
			{ Field.TileType.NORMAL, null, null, null, null, null, null, null, null, null, null, null, Field.TileType.NORMAL },
			{ Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.SPECIAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.SPECIAL, Field.TileType.NORMAL, Field.TileType.NORMAL },
				{ Field.TileType.NORMAL, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, Field.TileType.NORMAL },
							{ Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL },

		};

		var fieldSizePixels = 128;
		Field previousField = null;
		Field[,] tileMap = new Field[boardLayout.GetLength(0), boardLayout.GetLength(1)];

		// Iterate over the 2D board layout and create valid tiles
		for (int y = 0; y < boardLayout.GetLength(0); y++)
		{
			for (int x = 0; x < boardLayout.GetLength(1); x++)
			{
				var tileType = boardLayout[y, x];
				
				if (tileType != null)
				{
					// Instantiate and set tile properties
					var currentField = (Field)GD.Load<PackedScene>("res://scenes/Field.tscn").Instantiate();
					currentField.tileType = tileType.Value;
					currentField.Position = new Vector2(x * fieldSizePixels, y * fieldSizePixels);
					
					// Add tile to the tile map and to the list of tiles
					tileMap[y, x] = currentField;
					tiles.Add(currentField);
					AddChild(currentField);
				}
			}
		}

		// Now set neighbours for each valid tile
		for (int y = 0; y < boardLayout.GetLength(0); y++)
		{
			for (int x = 0; x < boardLayout.GetLength(1); x++)
			{
				if (tileMap[y, x] != null)
				{
					Field currentField = tileMap[y, x];
					
					// Check neighbours (up, down, left, right)
					if (y > 0 && tileMap[y - 1, x] != null)  // Up
						currentField.neighbours.Add(tileMap[y - 1, x]);
					if (y < boardLayout.GetLength(0) - 1 && tileMap[y + 1, x] != null)  // Down
						currentField.neighbours.Add(tileMap[y + 1, x]);
					if (x > 0 && tileMap[y, x - 1] != null)  // Left
						currentField.neighbours.Add(tileMap[y, x - 1]);
					if (x < boardLayout.GetLength(1) - 1 && tileMap[y, x + 1] != null)  // Right
						currentField.neighbours.Add(tileMap[y, x + 1]);
						
					GD.Print(currentField.neighbours.Count);
				}
			}
		}
	}
}
