using Godot;
using System;

public partial class Item : Area2D
{
	public enum ItemType { Boombox, BlueprintA, BlueprintB, BlueprintC, BlueprintD }

	public string Name { get; private set; }
	public Pawn Owner { get; set; }
	public string Description { get; private set; }
	public Texture2D Sprite { get; private set; }

	public ItemType type;

	[Signal]
	public delegate void ClickedEventHandler(Item item);

	public Item()
	{
		
	}
	
	public void setupItem(ItemType itemType, Pawn owner) {
		type = itemType;
		Owner = owner;
		SetupItem();
	}

	public override void _Ready()
	{
		var sprite = new Sprite2D();
		sprite.Texture = Sprite;
		sprite.Centered = false;
		AddChild(sprite);

		var collisionShape = new CollisionShape2D();
		var shape = new RectangleShape2D();
		shape.Size = new Vector2(Globals.FieldSizePixels, Globals.FieldSizePixels);
		collisionShape.Shape = shape;
		AddChild(collisionShape);

		InputPickable = true;
		Connect("input_event", new Callable(this, nameof(OnInputEvent)));
	}

	private void SetupItem()
	{
		switch (type)
		{
			case ItemType.Boombox:
				Name = "Lemurs' Boombox";
				Description = "Non stop playing pop hits. Use it to place it on a field. Pawn that enters that field loses its next turn.";
				Sprite = (Texture2D)GD.Load("res://Assets/boombox.png");
				break;
			case ItemType.BlueprintA:
				Name = "First blueprint";
				Description = "Gather 4 to rule them all";
				Sprite = (Texture2D)GD.Load("res://Assets/blueprintA.png");
				break;
			case ItemType.BlueprintB:
				Name = "Second blueprint";
				Description = "Gather 4 to rule them all";
				Sprite = (Texture2D)GD.Load("res://Assets/blueprintA.png");
				break;
			case ItemType.BlueprintC:
				Name = "Third blueprint";
				Description = "Gather 4 to rule them all";
				Sprite = (Texture2D)GD.Load("res://Assets/blueprintA.png");
				break;
			case ItemType.BlueprintD:
				Name = "Third blueprint";
				Description = "Gather 4 to rule them all";
				Sprite = (Texture2D)GD.Load("res://Assets/blueprintA.png");
				break;
		   
		   
		}
	}

	private void OnInputEvent(Node viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent &&
			mouseEvent.ButtonIndex == MouseButton.Left &&
			mouseEvent.Pressed)
		{
			EmitSignal(SignalName.Clicked, this);
			Action();
		}
	}

	public void Action()
	{
		switch (type)
		{
			case ItemType.Boombox:
				Owner.CurrentField.placable = new BoomboxPlacable(Sprite);
				Owner.items.Remove(this);
				Globals.Instance.Board.drawCurrentPawnItems();
				break;
			case ItemType.BlueprintA:
			case ItemType.BlueprintB:
			case ItemType.BlueprintC:
			case ItemType.BlueprintD:
				break;
		   
		}
	}
}
