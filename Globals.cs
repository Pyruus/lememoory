using Godot;
using System;
using System.Collections.Generic;

public partial class Globals : Node
{
	public static Globals Instance { get; private set; }

	public Pawn CurrentPlayerPawn { get; set; }
	public List<Field> Tiles { get; set; }
	public override void _Ready()
	{
		Instance = this;
	}
}
