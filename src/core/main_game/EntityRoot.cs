using Godot;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

public class EntityRoot : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private Cauldron _cauldron;
	private readonly NodePath _cauldronPath = "Cauldron";
	[Export] public int recipeNumber = 4;
	private RandomNumberGenerator _rng = new RandomNumberGenerator();
	//for the future maybe make this a list of lists?
	private List<string> chemicals = new List<string>{"Alkahest", "AquaFortis", "SaltPeter"};
	private List<string> animalProduct = new List<string>{"BearFat", "DragonScale", "EyeofNewt", "WyrmTongue"};
	private List<string> plant = new List<string>{"Acorn", "Dandelion", "Fig", "FigLeaves", "Mandrake", "MilkThistle"};
	private 
	enum IngredientBank{
		Acorn, 
		Alkahest, 
		AquaFortis, 
		BearFat,
		Dandelion,
		DragonScale, 
		EyeofNewt, 
		Fig, 
		FigLeaves,
		Mandrake, 
		MilkThistle, 
		PowderofAlgaroth, 
		SaltPeter, 
		WyrmTongue
	}
	public List<string> generatedRecipe = new List<string>{};
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//for the random recipe generation 
		_cauldron = GetNode<Cauldron>(_cauldronPath);
		/* 
		generatedRecipe.Add(chemicals[_rng.RandiRange(0, chemicals.Count-1)]);
		generatedRecipe.Add(animalProduct[_rng.RandiRange(0, animalProduct.Count-1)]);
		generatedRecipe.Add(plant[_rng.RandiRange(0, plant.Count-1)]);
		GD.Print(string.Join(", ", generatedRecipe));
		Random rng = new Random();
		int n = generatedRecipe.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			string value = generatedRecipe[k];
			generatedRecipe[k] = generatedRecipe[n];
			generatedRecipe[n] = value;
		}
		GD.Print(string.Join(", ", generatedRecipe)); */
		//recipe for testing
		generatedRecipe.Add("Alkahest");
		generatedRecipe.Add("AquaFortis");
		generatedRecipe.Add("BearFat");
		_cauldron.generatedRecipe = generatedRecipe;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      E 0:00:00.351   void System.ThrowHelper.ThrowArgumentOutOfRange_IndexException(): System.ArgumentOutOfRangeException: Index was out of range. Must be non-negative and less than the size of the collection.


//  }
	
}
