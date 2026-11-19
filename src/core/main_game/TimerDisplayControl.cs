using Godot;
using System;

public class TimerDisplayControl : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private TextureProgress _timerDisplay;
	private NodePath _timerDisplayPath = "TimerDisplay";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_timerDisplay = GetNode<TextureProgress>(_timerDisplayPath);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_GameTimerCopy_timeout()
	{
		_timerDisplay.Value += 1;
	}

}


