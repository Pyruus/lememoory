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
		this.currentPlayerPawn.Position = new Vector2(Globals.Instance.Tiles[0].Position.X + Globals.FieldSizePixels/2, Globals.Instance.Tiles[0].Position.Y + Globals.FieldSizePixels/2);
		this.currentPlayerPawn.CurrentField = Globals.Instance.Tiles[0];
		currentPlayerPawn.EmitEventResolvedSignal(title, modalMessage);
	}
}

public class PeacefulEvent: Event {
	public string title = "Peaceful silence";
	public string modalMessage = "Nothing happens in this forest. Really.";
	Pawn currentPlayerPawn;
	public PeacefulEvent(Pawn currentPlayer) {
		this.currentPlayerPawn = currentPlayer;
	}
	public override void resolve() {
		//nothing happens
		currentPlayerPawn.EmitEventResolvedSignal(title, modalMessage);
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
		var newItem = (Item)GD.Load<PackedScene>("res://Scenes/Item.tscn").Instantiate();
		newItem.setupItem(Item.ItemType.Boombox, this.currentPlayerPawn);
		this.currentPlayerPawn.items.Add(newItem);
		currentPlayerPawn.EmitEventResolvedSignal(title, modalMessage);
	}
	
}

public class AdditionalRoundEvent: Event {
	public string title = "Electric Scooter";
	public string modalMessage = "You find electric scooter. It is so fast, that you get an additional play.";
		Pawn currentPlayerPawn;
	public AdditionalRoundEvent(Pawn currentPlayer) {
		this.currentPlayerPawn = currentPlayer;
	}
	public override void resolve() {
		
		// Globals.Instance.NextPlayer = this.currentPlayerPawn;
		currentPlayerPawn.EmitEventResolvedSignal(title, modalMessage);
	}
}


public class MoveToRandomFieldEvent: Event {
	public string title = "Quantum teleportation portal";
	public string modalMessage = "You come across a strange device. You press the big red button and milliseconds later you find yourself in some strange place.";
		Pawn currentPlayerPawn;
	public MoveToRandomFieldEvent(Pawn currentPlayer) {
		this.currentPlayerPawn = currentPlayer;
	}
	public override void resolve() {
		
		var tilesCount = Globals.Instance.Tiles.Count;
		Random rnd = new Random();
		
		var randomNumber = rnd.Next(tilesCount);
		
		this.currentPlayerPawn.Position = new Vector2(Globals.Instance.Tiles[randomNumber].Position.X + Globals.FieldSizePixels/2, Globals.Instance.Tiles[randomNumber].Position.Y + Globals.FieldSizePixels/2);
		this.currentPlayerPawn.CurrentField = Globals.Instance.Tiles[randomNumber];
		currentPlayerPawn.EmitEventResolvedSignal(title, modalMessage);
	}
}
