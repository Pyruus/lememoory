using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Board : Node2D
{
	private List<Field> tiles = new List<Field>();
	private Pawn currentPlayer;
	private Dice dice;
	private Button rollButton;
	private QuestionMenu questionModal;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var currentPawn = GetNode<Pawn>("Pawn");
		Globals.Instance.CurrentPlayerPawn = currentPawn;
		rollButton = GetNode<Button>("RollButton");
		rollButton.Pressed += OnRollButtonPressed;
		
		CreateBoard();
		
		currentPlayer = GetNode<Pawn>("Pawn");
		currentPlayer.Position = tiles.FirstOrDefault().Position;
	}

	private void CreateBoard()
	{
		var boardLayout = new Field.TileType?[,] {
			{ Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.EVENT, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.EVENT, Field.TileType.NORMAL, Field.TileType.NORMAL },
			{ Field.TileType.NORMAL, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, Field.TileType.NORMAL },
			{ Field.TileType.NORMAL,  Field.TileType.NORMAL, Field.TileType.SPECIAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.SPECIAL, Field.TileType.NORMAL, Field.TileType.QUESTION },
			{ Field.TileType.NORMAL, null, null, null, null, null, Field.TileType.FINAL, null, null, null, null, null, Field.TileType.NORMAL },
			{ Field.TileType.QUESTION, Field.TileType.NORMAL, Field.TileType.SPECIAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.SPECIAL, Field.TileType.NORMAL, Field.TileType.NORMAL },
				{ Field.TileType.NORMAL, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, null, null, Field.TileType.NORMAL, null, Field.TileType.NORMAL },
							{ Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.QUESTION, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL,Field.TileType.EVENT, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL, Field.TileType.NORMAL },

		};

		var fieldSizePixels = 128;
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
					currentField.board = this;
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

		dice = (Dice)GD.Load<PackedScene>("res://Objects/dice.tscn").Instantiate();
		dice.Position = new Vector2(3 * fieldSizePixels, 3 * fieldSizePixels);
		rollButton.Position = new Vector2(3 * fieldSizePixels - rollButton.GetSize()[1]/2, 3 * fieldSizePixels + 50);
		AddChild(dice);
		
		Globals.Instance.Tiles = tiles;
	}

	private void OnRollButtonPressed()
	{
		foreach (var field in tiles)
		{
			field.Modulate = new Color("ffffff");
			field.Clickable = false;
		}
		var roll = dice.OnRollButtonPressed();
		rollButton.Disabled = true;
		var currentField = tiles.FirstOrDefault();
		var result = GetAvailableFields(roll, currentField);
		foreach (var field in result)
		{
			field.Modulate = new Color("85aebe");
			field.Clickable = true;
			field.InputPickable = true;
			field.InputEvent += MoveToField;
		}
	}

	private void MoveToField(Node node, InputEvent @event, long shapeIdx)
	{

				GD.Print($"Clicked on {node}!");

	}

	private List<Field> GetAvailableFields(int roll, Field startField)
	{
		var result = new List<Field>();
		DFS(startField, roll, new List<Field> { startField }, result);
		return result.Distinct().ToList();
	}
	
	private static void DFS(Field currentField, int remainingSteps, List<Field> path, List<Field> result)
	{
		if (remainingSteps == 0)
		{
			result.Add(currentField);
			return;
		}

		foreach (var neighbour in currentField.neighbours)
		{
			if (neighbour != currentField.previouslyVisitedField)
			{
				var newPath = new List<Field>(path) { neighbour };
				var previousLastVisited = neighbour.previouslyVisitedField;
				
				neighbour.previouslyVisitedField = currentField;
				DFS(neighbour, remainingSteps - 1, newPath, result);
				neighbour.previouslyVisitedField = previousLastVisited;
			}
		}
	}
	
	public void askQuestion() {
		GD.Print("ASKED");
		questionModal = (QuestionMenu)GD.Load<PackedScene>("res://Scenes/question_menu.tscn").Instantiate();
		questionModal.Position = new Vector2(0,0);
		AddChild(questionModal);
		questionModal.ShowRandomQuestion();
		
	}
	
}
