using Godot;
using System;

public interface Event
{
	public abstract void resolve();
}


public class MoveToCornerEvent:Event {
	Pawn currentPlayerPawn;
	public MoveToCornerEvent(Pawn currentPlayer) {
		this.currentPlayerPawn = currentPlayer;
	}
	
	public void resolve() {
		this.currentPlayerPawn.Position = new Vector2(0,0);
	}
}
