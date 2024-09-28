using Godot;
using System;
using System.Collections.Generic;
using System.Text.Json;

public partial class QuestionMenu : Control
{
	[Export]
	public PackedScene AnswerButtonScene { get; set; }
	[Signal]
	public delegate void AnsweredQuestionEventHandler(bool correct);

	private Label questionLabel;
	private VBoxContainer answerContainer;
	private Label feedbackLabel;
	private Button closeButton;

	private List<Button> answerButtons = new();
	private int correctAnswerIndex;

	private List<QuestionData> questions = new();
	private Random random = new Random();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		questionLabel = GetNode<Label>("PanelContainer/MarginContainer/VBoxContainer/QuestionLabel");
		answerContainer = GetNode<VBoxContainer>("PanelContainer/MarginContainer/VBoxContainer/AnswerContainer");
		feedbackLabel = GetNode<Label>("PanelContainer/MarginContainer/VBoxContainer/FeedbackLabel");
		closeButton = GetNode<Button>("PanelContainer/MarginContainer/VBoxContainer/CloseButton");

		closeButton.Pressed += OnCloseButtonPressed;

		feedbackLabel.Hide();
		closeButton.Hide();
		LoadQuestions();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void LoadQuestions()
	{
		var jsonFile = FileAccess.Open("res://Questions/questions.json", FileAccess.ModeFlags.Read);
		var jsonString = jsonFile.GetAsText();
		jsonFile.Close();

		var jsonDocument = JsonDocument.Parse(jsonString);
		var questionsArray = jsonDocument.RootElement.GetProperty("questions");

		foreach (var questionElement in questionsArray.EnumerateArray())
		{
			var questionData = new QuestionData
			{
				Question = questionElement.GetProperty("question").GetString(),
				Answers = new List<string>(),
				CorrectIndex = questionElement.GetProperty("correctIndex").GetInt32()
			};

			foreach (var answer in questionElement.GetProperty("answers").EnumerateArray())
			{
				questionData.Answers.Add(answer.GetString());
			}

			questions.Add(questionData);
		}
	}

	public void ShowRandomQuestion()
	{
		if (questions.Count == 0)
		{
			GD.PushError("No questions loaded!");
			return;
		}

		var randomQuestion = questions[random.Next(questions.Count)];
		ShowQuestion(randomQuestion.Question, randomQuestion.Answers.ToArray(), randomQuestion.CorrectIndex);
		questions.Remove(randomQuestion);
	}
	
	public void ShowQuestion(string question, string[] answers, int correctIndex)
	{
		// Set question text
		questionLabel.Text = question;

		// Clear existing answer buttons
		foreach (var button in answerButtons)
		{
			button.QueueFree();
		}
		answerButtons.Clear();

		// Create new answer buttons
		for (int i = 0; i < answers.Length; i++)
		{
			var answerButton = AnswerButtonScene.Instantiate() as Button;
			answerButton.Text = answers[i];
			int index = i;
			answerButton.Pressed += () => OnAnswerSelected(index);
			answerContainer.AddChild(answerButton);
			answerButtons.Add(answerButton);
		}

		correctAnswerIndex = correctIndex;

		// Reset and show the menu
		feedbackLabel.Hide();
		closeButton.Hide();
		Show();
	}

	private void OnAnswerSelected(int selectedIndex)
	{
		GD.Print(selectedIndex);
		// Disable all answer buttons
		foreach (var button in answerButtons)
		{
			button.Disabled = true;
		}

		// Show feedback
		if (selectedIndex == correctAnswerIndex)
		{
			feedbackLabel.Text = "Correct!";
			feedbackLabel.Modulate = Colors.Green;
			EmitSignal(SignalName.AnsweredQuestion, true);
		}
		else
		{
			feedbackLabel.Text = "Incorrect. The correct answer was: " + answerButtons[correctAnswerIndex].Text;
			feedbackLabel.Modulate = Colors.Red;
			EmitSignal(SignalName.AnsweredQuestion, false);
		}
		feedbackLabel.Show();

		// Show close button
		closeButton.Show();
	}

	private void OnCloseButtonPressed()
	{
		Hide(); // Hide the question menu
	}
}

public class QuestionData
{
	public string Question { get; set; }
	public List<string> Answers { get; set; }
	public int CorrectIndex { get; set; }
}
