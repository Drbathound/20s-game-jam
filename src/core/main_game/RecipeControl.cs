using Godot;
using System;
using System.CodeDom.Compiler;
using System.Dynamic;
using System.Net.NetworkInformation;

public class RecipeControl : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private ItemList _recipeList;
	private NodePath _recipeListPath = "RecipeList";
	//private DynamicFont _font;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_recipeList = GetNode<ItemList>(_recipeListPath);
		_recipeList.Visible = false;
		//_font = _recipeList.GetFont("font") as DynamicFont;
		//GD.Print("itemlist font is size " + _font.Size);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_RecipeIcon_toggled(bool button_pressed)
	{
		// Replace with function body.
		if(button_pressed){
			GD.Print("recipe should be visible");
			_recipeList.Visible = true;
		}else{
			_recipeList.Visible = false;
		}
		
	}
}



