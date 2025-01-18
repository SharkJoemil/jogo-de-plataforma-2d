using Godot;
using System;

public partial class Collectible : Area2D
{
	//Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Conecta o sinal body_entered ao método OnBodyEntered sendo Callable.From
		Connect("body_entered", Callable.From((Node body) => OnBodyEntered(body)));
	}

	private void OnBodyEntered(Node Body)
	{
		if(body is PlayerController player)
		{
			player.AddPoints(1);// Adiciona 1 ponto ao jogador
			QueueFree();//Remove o objeto colecionável da cena
		} 
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
