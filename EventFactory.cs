using Godot;
using System;

public partial class EventFactory
{
	
	public static Event getEvent() {
		Random rnd = new Random();
		
		var randomNumber = rnd.Next(5);
			 randomNumber = 4;
		switch(randomNumber) {
			case 0:
				return new MoveToCornerEvent(Globals.Instance.CurrentPlayerPawn);
			case 1:
				return new MoveToRandomFieldEvent(Globals.Instance.CurrentPlayerPawn);
			case 2:
				return new AdditionalRoundEvent(Globals.Instance.CurrentPlayerPawn);
			case 3:
				return new PeacefulEvent();
			case 4: 
				return new TrapGetEvent(Globals.Instance.CurrentPlayerPawn);
			default:
				return new MoveToCornerEvent(Globals.Instance.CurrentPlayerPawn);
		}
	}
}
