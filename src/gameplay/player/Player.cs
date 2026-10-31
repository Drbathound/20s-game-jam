using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class Player : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	//private var viewport_size = GetViewport().GetVisibleRect().Size;
	private AnimatedSprite _animatedSprite;
	private readonly NodePath _animatedSpritePath = "AnimatedSprite";
	[Export] public Vector2 ScreenSize = new Vector2(320,180);
	[Export] public Vector2 SpriteOffset = new Vector2(8f,8f);
	public int Speed = 100;
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
		Play_Animation(_velocity);
		
		//GD.Print("direction x: " + _velocity.x + " direction y: " + _velocity.y);
	}

	public override void _PhysicsProcess(float delta)
	{
		GetInput();
		KinematicCollision2D collision = MoveAndCollide(_velocity * delta);
		Vector2 clampedPosition = Position;
		clampedPosition.x = Mathf.Clamp(clampedPosition.x, SpriteOffset.x, ScreenSize.x - SpriteOffset.x);
		clampedPosition.y = Mathf.Clamp(clampedPosition.y, SpriteOffset.y, ScreenSize.y - SpriteOffset.y);
		Position = clampedPosition;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite>(_animatedSpritePath);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void Play_Animation(Vector2 dir){
		if(dir.x<0){
			_animatedSprite.Play("move_left");
		}else if(dir.x>0){
			_animatedSprite.Play("move_right");
		}else if(dir.y>0){
			_animatedSprite.Play("move_down");
		}else if(dir.y<0){
			_animatedSprite.Play("move_up");
		}
		if(dir.x == 0 && dir.y == 0){
			_animatedSprite.Play("idle");
		}
		
		
		
		
	}
}
