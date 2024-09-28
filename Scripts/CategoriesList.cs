using Godot;
using System;

public partial class CategoriesList : MenuButton
{
	private string[] options = { "General Knowledge", "Sports", "Kids" };
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PopupMenu popup = GetPopup();
		
		foreach (var option in options)
		{
			popup.AddItem(option);  // Correctly use AddItem on PopupMenu
		}
		
		// Connect the "item_selected" signal to a handler function
		popup.Connect("id_pressed", new Callable(this, nameof(OnItemSelected)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnItemSelected(int id)
	{
		switch (id)
		{
			case 0:
				Globals.SelectedQuestionsCategory.name = "General Knowledge";
				Globals.SelectedQuestionsCategory.jsonName = "questions.json";
				break;
			case 1:
				Globals.SelectedQuestionsCategory.name = "Sports";
				Globals.SelectedQuestionsCategory.jsonName = "questions_sports.json";
				break;
			case 2:
				Globals.SelectedQuestionsCategory.name = "Kids";
				Globals.SelectedQuestionsCategory.jsonName = "questions_kids.json";
				break;
		}
	}
}
