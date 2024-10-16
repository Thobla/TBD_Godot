using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public AnimatedSprite2D sprite = null;

	public override void _Ready()
		{
			sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		}

		

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("move_left", "move_right", "jump", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			// Flips the player
			sprite.FlipH = direction.X < 0;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		//animations:
		if (!IsOnFloor()){
			sprite.Play("spin");
		}
		else{
			if(direction.X != 0){
				sprite.Play("walk");
			}
			else{sprite.Play("default");
			};
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
