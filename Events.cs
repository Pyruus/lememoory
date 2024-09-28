using Godot;
using System;

public abstract class Event
{
	public abstract void resolve();
	string modalMessage;
	string title;
}


public class MoveToCornerEvent:Event {
	public string title = "Top left corner teleportation";
	public string modalMessage = "While chasing a bright light in the sky, you end up in unexplorered part of the forest";
	Pawn currentPlayerPawn;
	public MoveToCornerEvent(Pawn currentPlayer) {
		this.currentPlayerPawn = currentPlayer;
	}
	
	public override void resolve() {
		this.currentPlayerPawn.Position = new Vector2(0,0);
		this.currentPlayerPawn.CurrentField = Globals.Instance.Tiles[0];
	}
}

public class PeacefulEvent: Event {
	public string title = "Peaceful silence";
	public string modalMessage = "Nothing happens in this forest. Really.";
	public override void resolve() {
		//nothing happens
	}
}

public class TrapGetEvent: Event {
	public string title = "Lemur Boombox";
	public string modalMessage = "You find an old Lemur Boombox playing Pop hits from 70's. If another lemur finds it playing, he will surely spend some time dancing.";
	
	Pawn currentPlayerPawn;
	public TrapGetEvent(Pawn currentPlayer) {
		this.currentPlayerPawn = currentPlayer;
	}
	
	public override void resolve() {
		//todo add item to current player backpack
	}
	
}
