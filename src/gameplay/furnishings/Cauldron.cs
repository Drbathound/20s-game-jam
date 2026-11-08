using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;

public class Cauldron : KinematicBody2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	//[Export] public int ingredientCount = 3;
	private AnimatedSprite _animatedSprite;
	private readonly NodePath _animatedSpritePath = "AnimatedSprite";
	private Timer _timer;
	private readonly NodePath _timerPath = "Timer";
	[Export] public List<string> generatedRecipe;
	private List<string> _contents = new List<string>();
	private Player _player;
	private readonly NodePath _playerPath = "../Player";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>(_playerPath);
		_player.Connect(nameof(Player.PlacedIngredient), this, nameof(OnPlacedIngredient));
		_animatedSprite = GetNode<AnimatedSprite>(_animatedSpritePath);
		_animatedSprite.Play("default");
		_timer = GetNode<Timer>(_timerPath);
		//GD.Print(_player.Name);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		
	}
	private void OnPlacedIngredient(string ingredient){
		GD.Print("cauldron recieved ingredient " + ingredient);
		_contents.Add(ingredient);
		if(_contents.Count == generatedRecipe.Count){
			CheckContents();
			string printIngredients = "";
			for(int i = 0; i<_contents.Count; i++){
				printIngredients += _contents[i];
			}
			GD.Print("contents has" + printIngredients);
			
		}
		GD.Print("contents has" + string.Join(" ,", _contents));
		//if()
	}
	private void CheckContents(){
		if(_contents.SequenceEqual(generatedRecipe)){
			GD.Print("recipe complete");
			_animatedSprite.Play("good_recipe");
		}else{
			GD.Print("recipe done wrong");
			_animatedSprite.Play("bad_recipe");
			_timer.Start();
			_contents.Clear();
			//play bad recipe ingredient
		}
	}
	private void _on_Timer_timeout()
	{
		_animatedSprite.Play("default");
		// Replace with function body.
	}
}



