using Godot;
using System;
using System.Collections.Generic;
public partial class Field : Area2D
{
	[Signal]
	public delegate void ClickedEventHandler();
	
	private CollisionShape2D collisionShape;
	
	public enum TileType { QUESTION, NORMAL, SPECIAL, EVENT,FINAL }
	public TileType tileType = TileType.NORMAL;
	
	public List<Field> neighbours = new List<Field>();
	public Field previouslyVisitedField = null;
	public bool Clickable = false;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		setTileTexture();
		InputPickable = true;	
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");
		InputEvent += OnInputEvent;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		GD.Print($"Clicked on {Name}!");
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.ButtonIndex == MouseButton.Left &&
			mouseEvent.Pressed )
		{
				GD.Print($"Clicked on {Name}!");
				EmitSignal(SignalName.Clicked);
		}
	}
	
	private bool IsPointInShape(Vector2 point)
	{
		if (collisionShape == null) return false;

		var shape = collisionShape.Shape;
		var localPoint = ToLocal(point);

		switch (shape)
		{
			case CircleShape2D circle:
				return localPoint.LengthSquared() <= circle.Radius * circle.Radius;
			case RectangleShape2D rectangle:
				var extents = rectangle.Size / 2;
				return Mathf.Abs(localPoint.X) <= extents.X && Mathf.Abs(localPoint.Y) <= extents.Y;
			// Add more cases for other shape types if needed
			default:
				GD.PushWarning($"Unsupported shape type for {Name}");
				return false;
		}
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
			case TileType.EVENT:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_magia.png");
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
		
		
	}
	
	public void getGoalPiece() {
		
	}
	
	public void checkWinningCondition() {
		
	}
	
	public void getEvent() {
		EventFactory.getEvent().resolve();
	}
}
