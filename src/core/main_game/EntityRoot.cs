using Godot;
using System;
using System.Collections.Generic;

public class EntityRoot : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export] private Vector2 _minPos = new Vector2(0, 0);
	[Export] private Vector2 _maxPos = new Vector2(800, 600);
	//[Export] private readonly PackedScene _entityScene;
	[Export] private float _minSeparation = 20f;
	[Export] private int _maxAttempts = 10;
	[Export] private string _scenePath = "res://src/gameplay/ingredients/";
	
	private RandomNumberGenerator _rng;
	private List<Vector2> _placedPositions = new List<Vector2>();
	private List<PackedScene> _scenes;
	private int _spawnIndex = 0;
	private Timer _spawnTimer;

	public void Spawn()
	{
		/*
		var entity = _entityScene.Instance<KinematicBody2D>();

		// Random position within the rectangle
		float x = _rng.RandfRange(_minPos.x, _maxPos.x);
		float y = _rng.RandfRange(_minPos.y, _maxPos.y);
		entity.Position = new Vector2(x, y);

		// Add to the spawner's parent (the world), not the spawner itself
		AddChild(entity);
		entity.AddToGroup("ingredient");
		*/
		/*
		_placedPositions.Clear();

		foreach (var scene in _scenes)
		{
			if (scene == null) continue;

			Vector2 pos = OverlapPrevention();
			var entity = (KinematicBody2D)scene.Instance();
			entity.Position = pos - Position;
			AddChild(entity);
			//entity.AddToGroup("ingredient");
		}
		*/

		if (_spawnIndex >= _scenes.Count)
		{
			_spawnTimer.Stop();
			GD.Print("All entities spawned.");
			return;
		}

		var scene = _scenes[_spawnIndex];
		if (scene != null)
		{
			Vector2 pos = OverlapPrevention();
			var entity = (KinematicBody2D)scene.Instance();
			entity.Position = pos - Position;
			AddChild(entity);
		}

		_spawnIndex++;
	}

	private List<PackedScene> IngredientFolderLoad(string path)
	{
		var scenes = new List<PackedScene>();
		var dir = new Godot.Directory();

		if (dir.Open(path) == Error.Ok)
		{
			dir.ListDirBegin(true, true);
			
			string fileName;
			while ((fileName = dir.GetNext()) != " ")
			{
				if (dir.CurrentIsDir()) continue;
				if(!fileName.EndsWith(".tscn")) continue;

				GD.Print("Attempting to load: " + path + fileName);
				OS.DelayMsec(100);
				
				var scene = (PackedScene)GD.Load(path + fileName);
				if(scene != null)
				{
					scenes.Add(scene);
					GD.Print(" -> Success");
				}
				else
				{
					GD.PrintErr(" -> Failed");
				}

			}
			dir.ListDirEnd();
		}
		else
		{
			GD.PrintErr("Could not open directory: " + path);
		}
		
		return scenes;
	}

	private Vector2 OverlapPrevention()
	{
		for (int attempt = 0; attempt < _maxAttempts; attempt++)
		{
			float x = _rng.RandfRange(_minPos.x, _maxPos.x);
			float y = _rng.RandfRange(_minPos.y, _maxPos.y);
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

		// Fallback: return a random position even if overlapping
		GD.PrintErr("Could not find non-overlapping position, using fallback.");
		Vector2 fallback = new Vector2(
			_rng.RandfRange(_minPos.x, _maxPos.x),
			_rng.RandfRange(_minPos.y, _maxPos.y)
		);
		_placedPositions.Add(fallback);
		return fallback;
	}

	private void SpawnDeferred()
	{
		GD.Print("Spawning " + _scenes.Count + " entities...");
		Spawn();
		GD.Print("Done spawning.");
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		try
		{
			_rng = new RandomNumberGenerator();
			_rng.Randomize();
			_scenes = IngredientFolderLoad(_scenePath);

			GD.Print("Found " + _scenes.Count + " scenes");
			foreach (var s in _scenes)
				GD.Print(" " + s.ResourcePath);

			/*
			_spawnTimer = new Timer { WaitTime = 0.05f, OneShot = false };
			_spawnTimer.Connect("timeout", this, nameof(Spawn));
			AddChild(_spawnTimer);
			_spawnTimer.Start();
			*/
		}
		catch (Exception e)
		{
			GD.PrintErr("SPAWNER CRASH: " + e.Message + "\n" + e.StackTrace);
		}


	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
