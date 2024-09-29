using Godot;
using System;
using System.Collections.Generic;
public partial class Field : Area2D
{
	[Signal]
	public delegate void ClickedEventHandler(Field field);

	private CollisionShape2D collisionShape;

	public enum TileType { QUESTION, NORMAL, SPECIAL1, SPECIAL2, SPECIAL3, SPECIAL4, EVENT, FINAL }
	public TileType tileType = TileType.NORMAL;

	public List<Field> neighbours = new List<Field>();
	public Field previouslyVisitedField = null;
	public bool Clickable = true;
	public Board board;
	public Placable placable;
	

	public override void _Ready()
	{
		
		setTileTexture();
		InputPickable = true;
		collisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

		// Connect the input event signal
		Connect("input_event", new Callable(this, nameof(OnInputEvent)));
		Connect("mouse_entered", new Callable(this, nameof(OnMouseEntered)));
		Connect("mouse_exited", new Callable(this, nameof(OnMouseExited)));
	}


	public void OnInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.ButtonIndex == MouseButton.Left &&
			mouseEvent.Pressed &&
			Clickable)
		{
			GD.Print($"Clicked on {Name}!");
			EmitSignal(SignalName.Clicked, this);
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
			case TileType.SPECIAL1:
			case TileType.SPECIAL2:
			case TileType.SPECIAL3:
			case TileType.SPECIAL4:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pole_magia.png");
				break;
			case TileType.EVENT:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/pustynia_skalki.png");
				break;
			case TileType.FINAL:
				sprite.Texture = (Texture2D)GD.Load("res://Assets/final.png");
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
			case TileType.SPECIAL1:
				askQuestion(1);
				break;
			case TileType.SPECIAL2:
				askQuestion(2);
				break;
			case TileType.SPECIAL3:
				askQuestion(3);
				break;
			case TileType.SPECIAL4:
				askQuestion(4);
				break;
			case TileType.FINAL:
				checkWinningCondition(Globals.Instance.CurrentPlayerPawn);
				break;
			case TileType.EVENT:
				getEvent();
				break;
			}
	}
	public void askQuestion() {
		board.askQuestion();
	}
	public void askQuestion(int specialIndex) {
		board.askQuestion(specialIndex);
	}
	
	public void getGoalPiece() {
		
	}
	
	public void checkWinningCondition(Pawn currentPlayer) {
		if (currentPlayer.hasFirstBluePrint &&
			currentPlayer.hasSecondBluePrint &&
			currentPlayer.hasThirdBluePrint &&
			currentPlayer.hasFourthBluePrint)
		{
			GD.Print("WON!");
		}
	}
	
	public void getEvent() {
		EventFactory.getEvent().resolve();
	}
	
	private void OnMouseEntered()
	{
		if (Clickable)
		{
			Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);	
		}
		else
		{
			Input.SetDefaultCursorShape();
		}
	}

	private void OnMouseExited()
	{
		if (Input.GetCurrentCursorShape() == Input.CursorShape.PointingHand)
		{
			Input.SetDefaultCursorShape();
		}
	}
}
