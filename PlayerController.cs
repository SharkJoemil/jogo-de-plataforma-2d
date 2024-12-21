using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	//Varievei para o movimento do personagem

	private float _Speed = 300.0f;

	private float _JumpVelovity= -400.0f

	private float gravity = 1000.0f;

	//Referencias para os sprites do jogador 

	private TileMap _tileMap;

	private Sprite2D _idleSprite;

	private AnimatedSprite2D _walkingSprite;

	private AnimatedSprite2D _attackSprite;

	private AnimatedSprite2D _jumpSprite;		

	//Variavel para a velocidade do ator

	private Vector2 _velocity = Vector2.zero;

	//Variavel Booleana para o ataque 

	private bool _isAttacking = false;

	//Função Chamada quando o nó está pronto
	public override void Ready()
	{
		//Guarda a referencia dos nodes nas variaveis para o controlo do personagem
		_tileMap = GetNode<TileMap>("root/Main/TileMap");
		_idleSprite = GetNode<Sprite2D>("Idle");
		_attackSprite = GetNode<AnimatedSprite2D>("AttackSprite");
		_jumpSprite = GetNode<AnimatedSprite2D>("JumpSprite");
		_walkingSprite = GetNode<AnimatedSprite2D>("WalkSprite");

		//Defenir a visibilidade dos sprites no inicio da cena
		_walkingSprite.visible = false;
		_attackSprite.visible = false;
		_jumpSprite.visible = false;
		_idleSprite.visible = true;
	}

	public override void _PhysicsProcess(double delta)
	{

		//Dar  reset a velocidade horizontal
		_velocity.X = 0;

		//Se estar a pressionar na tecla esquerda, andas para a esquerda
		if(Input.IsActionJustPressed("ui_left"))
		{
			_velocity.X = -_Speed;
		}

		//Se estar a pressionar na tecla direita, andas para a direita
		if(Input.IsActionJustPressed("ui_right"))
		{
			_velocity.X = -_Speed;
		}

		//Aplicar a Gravidade ao Jogador
		_velocity.Y += gravity * (float)delta;
		_jumpSprite.visible = false;


	if(Input.IsActionJustPressed("ui_up"))
	{
		_velocity.Y = _JumpVelovity;
		_jumpSprite.visible = true; //Mostra o sprite de salto
		_isAttacking = false; //cancelamos o ataque se quisermos saltar

		_idleSprite.visible	= true;
		_walkingSprite.visible = false;
		_attackSprite.visible = false;
		
		_jumpSprite.Play("jump"); //inicia a animaçao de jump
		}


		if(Input.IsActionJustPressed("ui_attack"))
		{
			_isAttacking = true;
			_jumpSprite.visible = false;

			_idleSprite.visible = false;
			_walkingSprite.visible = false;
			_attackSprite.visible = true;

			_attackSprite.Play("attack")
		}
		else
		{
			_isAttacking = false;
			_attackSprite.visible = false;
		}

		_UpdateSpriteRenderer();



	}

	//funçao para atualizar o sprite
	private void _UpdateSpriteRenderer(float velX)
	{
		bool walking = Mathf.Abs(velX) > 0 && !_isAttacking && !_jumpSprite.visible;

		bool idle = !walking && !_isAttacking && !_jumpSprite.visible;

		_idleSprite.visible = idle;
		_walkingSprite.visible = walking;

		//mostrar ataque de ataque se estivermos a atacar
		if(_isAttacking)
		{
			_attackSprite.visible = true;
			_attackSprite.Play("attack");
		}
		else
		{
			_attackSprite.visible = true;
			_attackSprite.Stop();
		}
		//mostrar sprite de pulo se estivermos a pular
		if(_jumpSprite.visible)
		{
			_jumpSprite.visible = true;
			_jumpSprite.Play("jump");
		}
		else
		{
			_jumpSprite.visible = true;
			_jumpSprite.Stop();
		}

		if(walking)
		{
			_walkingSprite.Play();
			_walkingSprite.FlipH = velx < 0;
		}
	}
}






/*public partial class PlayerController : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	//Varievei para o movimento do personagem

	private float _Speed = 300.0f;

	private float _JumpVelovity= -400.0f

	private float gravity = 1000.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}*/