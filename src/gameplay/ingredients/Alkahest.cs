using Godot;
using System;

public class Alkahest : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private KinematicBody2D _Player;
	private readonly NodePath _PlayerPath = "root/MainGame/World/EntityRoot/Player";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Player = GetNode<KinematicBody2D>(_PlayerPath);
	}
	/* private void _on_Alkahest_body_entered(object body)
	{
		if(body is _Player){
			Player.holding = true;
		}
		// Replace with function body.
	} */

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}


