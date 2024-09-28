using Godot;
using System;

public abstract class Placable
{
	public abstract void onEnter(Pawn pawn);
	public Texture2D sprite;
	public Field ownerField;	
}

public class BoomboxPlacable: Placable {
	public BoomboxPlacable(Texture2D sprite) {
			this.sprite = sprite;
	}
	public override void onEnter(Pawn pawn){
		pawn.skipsNextTurn = true;
		this.ownerField.placable = null;
	}
}
