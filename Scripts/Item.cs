using Godot;
using System;

public abstract class Item 
{
	public string name;
	public Pawn owner;
	public string description;
	public Texture2D sprite;
	public abstract void action();
}

public class Boombox: Item {
	public string name = "Lemurs' Boombox";
	public string description = "Non stop playing pop hits. Use it to place it on a field. Pawn that enters that field looses its next turn.";
	public Boombox(string textureName, Pawn owner) {
		this.sprite = (Texture2D)GD.Load("res://Assets/" + textureName);
		this.owner = owner;
	}
	
	public override void action() {
		this.owner.CurrentField.placable = new BoomboxPlacable(this.sprite);
	}
}
