using Godot;
using System;

public partial class Globals : Node
{
	public static Globals Instance { get; private set; }

	public Pawn CurrentPlayer { get; set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
