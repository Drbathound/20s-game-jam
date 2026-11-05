using Godot;
using System.Collections.Generic;

public class IngredientRandomization : Node2D
{
	[Export] private Vector2 _topLeft = new Vector2(0, 0);
	[Export] private Vector2 _bottomRight = new Vector2(800, 600);
	[Export] private float _minSeparation = 60f;
	[Export] private int _maxAttempts = 50;
	[Export] private string _scenePath = "res://src/gameplay/ingredients/";

	private RandomNumberGenerator _rng;
	private List<Vector2> _placedPositions = new List<Vector2>();
	private List<PackedScene> _scenes = new List<PackedScene>();

	public override void _Ready()
	{
		_rng = new RandomNumberGenerator();
		_rng.Randomize();
		_scenes = IngredientFolderLoad(_scenePath);

		SpawnAll();
	}

	public void SpawnAll()
	{
		_placedPositions.Clear();

		for (int i = 0; i < _scenes.Count; i++)
		{
			var scene = _scenes[i];
			if (scene == null) continue;

			Vector2 pos = OverlapPrevention();

			var entity = (KinematicBody2D)scene.Instance();

			entity.Position = pos - Position;

			AddChild(entity);
            entity.AddToGroup("ingredient");
		}
	}

	private List<PackedScene> IngredientFolderLoad(string path)
	{
		var scenes = new List<PackedScene>();

		var dir = new Godot.Directory();
		Error result = dir.Open(path);

		if (result == Error.Ok)
		{
			dir.ListDirBegin(true, true);

			string fileName;
			int count = 0;
			while ((fileName = dir.GetNext()) != "")
			{
				count++;

				if (dir.CurrentIsDir()) continue;
				if (!fileName.EndsWith(".tscn")) continue;

				var scene = (PackedScene)GD.Load(path + fileName);

				if (scene != null)
					scenes.Add(scene);
			}

			dir.ListDirEnd();
		}
		else
		{
			GD.PrintErr("FAILED to open directory");
		}

		return scenes;
	}

	private Vector2 OverlapPrevention()
	{
		for (int attempt = 0; attempt < _maxAttempts; attempt++)
		{
			float x = _rng.RandfRange(_topLeft.x, _bottomRight.x);
			float y = _rng.RandfRange(_topLeft.y, _bottomRight.y);
			Vector2 candidate = new Vector2(x, y);

			bool overlaps = false;
			foreach (var placed in _placedPositions)
			{
				if (candidate.DistanceTo(placed) < _minSeparation)
				{
					overlaps = true;
					break;
				}
			}

			if (!overlaps)
			{
				_placedPositions.Add(candidate);
				return candidate;
			}
		}

		Vector2 fallback = new Vector2(
			_rng.RandfRange(_topLeft.x, _bottomRight.x),
			_rng.RandfRange(_topLeft.y, _bottomRight.y)
		);
		_placedPositions.Add(fallback);
		return fallback;
	}
}   
