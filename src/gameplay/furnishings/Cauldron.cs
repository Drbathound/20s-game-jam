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
	[Export] public List<string> generatedRecipe;
	private List<string> _contents = new List<string>();
	private Player _player;
	private readonly NodePath _playerPath = "../Player";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_player = GetNode<Player>(_playerPath);
		_player.Connect(nameof(Player.PlacedIngredient), this, nameof(OnPlacedIngredient));
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
		//if()
	}
	private void CheckContents(){
		if(_contents.SequenceEqual(generatedRecipe)){
			GD.Print("recipe complete");
		}else{
			GD.Print("recipe done wrong");
			//play bad recipe ingredient
		}
	}
}
