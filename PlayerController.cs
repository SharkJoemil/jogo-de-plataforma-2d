using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	//Varievei para o movimento do personagem

	private float _Speed = 300.0f;

	private float _JumpVelovity= -400.0f;

	private float gravity = 1000.0f;

	//Referencias para os sprites do jogador 

	private TileMap _tileMap;

	private Sprite2D _idleSprite;

	private AnimatedSprite2D _walkingSprite;

	private AnimatedSprite2D _attackSprite;

	private AnimatedSprite2D _jumpSprite;		

	//Variavel para a velocidade do ator

	private Vector2 _velocity = Vector2.Zero;

	//Variavel Booleana para o ataque 

	private bool _isAttacking = false;

	private int _score = 0;

	//Função Chamada quando o nó está pronto
	public override void _Ready()
	{
		//Guarda a referencia dos nodes nas variaveis para o controlo do personagem
		_tileMap = GetNode<TileMap>("root/Main/TileMap");
		_idleSprite = GetNode<Sprite2D>("Idle");
		_attackSprite = GetNode<AnimatedSprite2D>("AttackSprite");
		_jumpSprite = GetNode<AnimatedSprite2D>("JumpSprite");
		_walkingSprite = GetNode<AnimatedSprite2D>("WalkSprite");

		//Defenir a visibilidade dos sprites no inicio da cena
		_walkingSprite.Visible = false;
		_attackSprite.Visible = false;
		_jumpSprite.Visible = false;
		_idleSprite.Visible = true;
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
		_jumpSprite.Visible = false;


	if(Input.IsActionJustPressed("ui_up"))
	{
		_velocity.Y = _JumpVelovity;
		_jumpSprite.Visible = true; //Mostra o sprite de salto
		_isAttacking = false; //cancelamos o ataque se quisermos saltar

		_idleSprite.Visible	= true;
		_walkingSprite.Visible = false;
		_attackSprite.Visible = false;
		
		_jumpSprite.Play("jump"); //inicia a animaçao de jump
		}


		if(Input.IsActionJustPressed("ui_attack"))
		{
			_isAttacking = true;
			_jumpSprite.Visible = false;

			_idleSprite.Visible = false;
			_walkingSprite.Visible = false;
			_attackSprite.Visible = true;

			_attackSprite.Play("attack");
		}
		else
		{
			_isAttacking = false;
			_attackSprite.Visible = false;
		}

		_UpdateSpriteRenderer(_velocity.X);
	


	}

	//funçao para atualizar o sprite
	private void _UpdateSpriteRenderer(float velX)
	{
		bool walking = Mathf.Abs(velX) > 0 && !_isAttacking && !_jumpSprite.Visible;

		bool idle = !walking && !_isAttacking && !_jumpSprite.Visible;

		_idleSprite.Visible = idle;
		_walkingSprite.Visible = walking;

		//mostrar ataque de ataque se estivermos a atacar
		if(_isAttacking)
		{
			_attackSprite.Visible = true;
			_attackSprite.Play("attack");
		}
		else
		{
			_attackSprite.Visible = true;
			_attackSprite.Stop();
		}
		//mostrar sprite de pulo se estivermos a pular
		if(_jumpSprite.Visible)
		{
			_jumpSprite.Visible = true;
			_jumpSprite.Play("jump");
		}
		else
		{
			_jumpSprite.Visible = true;
			_jumpSprite.Stop();
		}

		if(walking)
		{
			_walkingSprite.Play();
			_walkingSprite.FlipH = velX < 0;
		}
	}


	// Função para verificar se o jogador está atacando
	

	public void AddPoints(int points)
	{
		_score += points;
		GD.Print("Score: " + _score);
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