using Godot;
using System;
using System.Collections.Generic;

public partial class Pawn : Area2D
{
	[Export]
	public float Speed = 200.0f; // Adjust this to change movement speed
	
	[Signal]
	public delegate void EventResolvedEventHandler(string title, string description);

	public Field CurrentField;

	private Vector2 targetPosition;
	private bool isMoving = false;
	public List<Item> items = new List<Item>();
	
	public bool skipsNextTurn = false;

	public bool hasFirstBluePrint = true;
	public bool hasSecondBluePrint = true;
	public bool hasThirdBluePrint = true;
	public bool hasFourthBluePrint = true;

	public Sprite2D sprite;

	public override void _Ready()
	{
		targetPosition = GlobalPosition;

		sprite = GetNode<Sprite2D>("Lemur");
	}

	public override void _Process(double delta)
	{
		if (isMoving)
		{
			Vector2 direction = targetPosition - GlobalPosition;
			if (direction.Length() > 1)
			{
				GlobalPosition += direction.Normalized() * Speed * (float)delta;
			}
			else
			{
				GlobalPosition = targetPosition;
				isMoving = false;
			}
		}
	}

	public override void _Input(InputEvent @event)
	{
		
	}

	public void EmitEventResolvedSignal(string title, string description)
	{
		EmitSignal(SignalName.EventResolved, title, description);
	}
	
}
