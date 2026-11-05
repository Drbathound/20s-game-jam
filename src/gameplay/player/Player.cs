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
	private Sprite _acorn;
    private readonly NodePath _acornPath = "acorn";
	private Sprite _alkahest;
	private readonly NodePath _alkahestPath = "alkahest";
	private Sprite _aquaFortis;
	private readonly NodePath _aquaFortisPath = "aquaFortis";
    private Sprite _bearFat;
    private readonly NodePath _bearFatPath = "bearFat";
    private Sprite _dandelion;
    private readonly NodePath _dandelionPath = "dandelion";
    private Sprite _dragonScale;
    private readonly NodePath _dragonScalePath = "dragonScale";
    private Sprite _eyeOfNewt;
    private readonly NodePath _eyeOfNewtPath = "eyeOfNewt";
    private Sprite _fig;
    private readonly NodePath _figPath = "fig";
    private Sprite _figLeaves;
	private readonly NodePath _figLeavesPath = "figLeaves";
    private Sprite _mandrake;
    private readonly NodePath _mandrakePath = "mandrake";
    private Sprite _milkThistle;
    private readonly NodePath _milkThistlePath = "milkThistle";
    private Sprite _powderOfAlgaroth;
    private readonly NodePath _powderOfAlgarothPath = "powderOfAlgaroth";
    private Sprite _saltPeter;
    private readonly NodePath _saltPeterPath = "saltPeter";
    private Sprite _wyrmTongue;
    private readonly NodePath _wyrmTonguePath = "wyrmTongue";
    [Export] public bool holding = false;
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
		
		if(collision!= null){
			//GD.Print(collision);
			Node collider = (Node)collision.Collider;
			
			if(!holding){
				if(collider.IsInGroup("ingredient")){
					GD.Print(collider.Name);
					Set_Ingredient(collider.Name);
					holding = true;
				}
			}else{
				if(collider.Name == "Cauldron"){
					//_alkahest.Visible = false;
					//_aquaFortis.Visible = false;
					IngredientsFalse();
					holding = false;
				}
			}
			
		}
		//_alkahest.Position = new Vector2(this.Position.x, this.Position.y - 8);
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite>(_animatedSpritePath);
		_acorn = GetNode<Sprite>(_acornPath);
		_alkahest = GetNode<Sprite>(_alkahestPath);
		//_alkahest.Visible = false;
		_aquaFortis = GetNode<Sprite>(_aquaFortisPath);
		//_aquaFortis.Visible = false;
        _bearFat = GetNode<Sprite>(_bearFatPath);
        _dandelion = GetNode<Sprite>(_dandelionPath);
        _dragonScale = GetNode<Sprite>(_dragonScalePath);
        _eyeOfNewt = GetNode<Sprite>(_eyeOfNewtPath);
        _fig = GetNode<Sprite>(_figPath);
        _figLeaves = GetNode<Sprite>(_figLeavesPath);
        _mandrake = GetNode<Sprite>(_mandrakePath);
		_milkThistle = GetNode<Sprite>(_milkThistlePath);
		_powderOfAlgaroth = GetNode<Sprite>(_powderOfAlgarothPath);
		_saltPeter = GetNode<Sprite>(_saltPeterPath);
		_wyrmTongue = GetNode<Sprite>(_wyrmTonguePath);
        IngredientsFalse();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void Play_Animation(Vector2 dir){
		if(holding){
			if(dir.x<0){
				_animatedSprite.Play("carry_left");
			}else if(dir.x>0){
				_animatedSprite.Play("carry_right");
			}else if(dir.y>0){
				_animatedSprite.Play("carry_down");
			}else if(dir.y<0){
				_animatedSprite.Play("carry_up");
			}
			if(dir.x == 0 && dir.y == 0){
				_animatedSprite.Play("carry_idle");
			}
		}else{
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
	private void IngredientsFalse(){
		_alkahest.Visible = false;
		_aquaFortis.Visible = false;
        _acorn.Visible = false;
        _bearFat.Visible = false;
        _dandelion.Visible = false;
        _dragonScale.Visible = false;
		_eyeOfNewt.Visible = false;
        _fig.Visible = false;
		_figLeaves.Visible = false;
		_mandrake.Visible = false;
		_milkThistle.Visible = false;
		_powderOfAlgaroth.Visible = false;
		_saltPeter.Visible = false;
		_wyrmTongue.Visible = false;
    }
	private void Set_Ingredient(string ingredientName){
		switch (ingredientName){
			case "Acorn":
                _acorn.Visible = true;
                break;
            case "Alkahest":
				_alkahest.Visible = true;
				break;
			case "AquaFortis":
				_aquaFortis.Visible = true;
				break;
			default:
				break;
			case "BearFat":
                _bearFat.Visible = true;
                break;
			case "Dandelion":
                _dandelion.Visible = true;
                break;
			case "DragonScale":
                _dragonScale.Visible = true;
                break;
			case "EyeofNewt":
                _eyeOfNewt.Visible = true;
                break;
			case "Fig":
                _fig.Visible = true;
                break;
			case "FigLeaves":
                _figLeaves.Visible = true;
                break;
			case "Mandrake":
                _mandrake.Visible = true;
                break;
			case "MilkThistle":
                _milkThistle.Visible = true;
                break;
			case "PowderofAlgaroth":
                _powderOfAlgaroth.Visible = true;
                break;
			case "SaltPeter":
                _saltPeter.Visible = true;
                break;
			case "WyrmTongue":
                _wyrmTongue.Visible = true;
                break;		
        }
	}
}
