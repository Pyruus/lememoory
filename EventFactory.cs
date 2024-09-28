using Godot;
using System;

public partial class EventFactory
{
	
	public static Event getEvent() {
		Random rnd = new Random();
		
		var randomNumber = rnd.Next(10);
		switch(randomNumber) {
			case 0:
				return new MoveToCornerEvent(Globals.Instance.CurrentPlayerPawn);
			default:
				return new MoveToCornerEvent(Globals.Instance.CurrentPlayerPawn);
		}
	}
}
