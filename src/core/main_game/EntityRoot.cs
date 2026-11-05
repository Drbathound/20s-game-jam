using Godot;
using System;

public class EntityRoot : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export] private Vector2 _minPos = new Vector2(0, 0);
	[Export] private Vector2 _maxPos = new Vector2(800, 600);
	[Export] private readonly PackedScene _entityScene;
	private RandomNumberGenerator _rng;

	public void Spawn()
	{
		var entity = _entityScene.Instance<KinematicBody2D>();

		// Random position within the rectangle
		float x = _rng.RandfRange(_minPos.x, _maxPos.x);
		float y = _rng.RandfRange(_minPos.y, _maxPos.y);
		entity.Position = new Vector2(x, y);

		// Add to the spawner's parent (the world), not the spawner itself
		AddChild(entity);
		entity.AddToGroup("ingredient");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rng = new RandomNumberGenerator();
		_rng.Randomize();
		Spawn();
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
