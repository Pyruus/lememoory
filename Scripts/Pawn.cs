using Godot;
using System;

public partial class Pawn : Area2D
{
	[Export]
	public float Speed = 200.0f; // Adjust this to change movement speed

	public Field CurrentField;

	private Vector2 targetPosition;
	private bool isMoving = false;

	public override void _Ready()
	{
		targetPosition = GlobalPosition;
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
}
