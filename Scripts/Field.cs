using Godot;
using System;
using System.Collections.Generic;
public partial class Field : Area2D
{
	[Signal]
	public delegate void ClickedEventHandler();

	private CollisionShape2D collisionShape;

	public enum TileType { QUESTION, NORMAL, SPECIAL, EVENT, FINAL }
	public TileType tileType = TileType.NORMAL;

	public List<Field> neighbours = new List<Field>();
	public Field previouslyVisitedField = null;
	public bool Clickable = true;
	public Board board;
	

	public override void _Ready()
	{
		
		setTileTexture();
		InputPickable = true;
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

		// Connect the input event signal
		Connect("input_event", new Callable(this, nameof(OnInputEvent)));
	}


	public void OnInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.ButtonIndex == MouseButton.Left &&
			mouseEvent.Pressed)
		{
			GD.Print($"Clicked on {Name}!");
			EmitSignal(SignalName.Clicked);
			onEnter(); // Call onEnter when clicked
		}
	}
	
	private void setTileTexture() {
		var sprite = GetNode<Sprite2D>("Sprite2D");

		
		switch (tileType)
		{
			case TileType.QUESTION:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/dzewo.png");
				break;
			case TileType.NORMAL:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_1.png");
				break;
			case TileType.SPECIAL:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_magia.png");
				break;
			case TileType.EVENT:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pustynia_skalki.png");
				break;
			case TileType.FINAL:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_magia.png");
				break;
		}
		
		float scaleX = 128.0f / sprite.Texture.GetWidth();
		float scaleY = 128.0f / sprite.Texture.GetHeight();
		sprite.Scale = new Vector2(scaleX, scaleY);
	}
	
	public void onEnter() {
			switch(tileType) {
				case TileType.QUESTION:
				askQuestion();
				break;
			case TileType.NORMAL:
				break;
			case TileType.SPECIAL:
				getGoalPiece();
				break;
			case TileType.FINAL:
				checkWinningCondition();
				break;
			case TileType.EVENT:
				getEvent();
				break;
			}
	}
	public void askQuestion() {
		board.askQuestion();
	}
	
	public void getGoalPiece() {
		
	}
	
	public void checkWinningCondition() {
		
	}
	
	public void getEvent() {
		EventFactory.getEvent().resolve();
	}
}
