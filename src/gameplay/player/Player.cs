using Godot;
using System;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public int Speed = 250;
	private Vector2 _velocity = new Vector2();

	public void GetInput()
	{
		// Detect up/down/left/right keystate and only move when pressed
		_velocity = new Vector2();

		/* if (Input.IsActionPressed("ui_right"))
			_velocity.x += 1;

		if (Input.IsActionPressed("ui_left"))
			_velocity.x -= 1;

		if (Input.IsActionPressed("ui_down"))
			_velocity.y += 1;

		if (Input.IsActionPressed("ui_up"))
			_velocity.y -= 1;
		_velocity = _velocity.Normalized() * Speed; */
		if (Input.IsActionPressed("right"))
			_velocity.x += 1;

		if (Input.IsActionPressed("left"))
			_velocity.x -= 1;

		if (Input.IsActionPressed("down"))
			_velocity.y += 1;

		if (Input.IsActionPressed("up"))
			_velocity.y -= 1;
		_velocity = _velocity.Normalized() * Speed;
	}

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		MoveAndCollide(_velocity * delta);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
