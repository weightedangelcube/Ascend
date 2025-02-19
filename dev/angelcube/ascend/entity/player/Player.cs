using dev.angelcube.ascend.util.logging;
using Godot;
using Vector2 = Godot.Vector2;

namespace dev.angelcube.ascend.entity.player {
	public partial class Player : CharacterBody2D {
		private AnimatedSprite2D _sprite;

		private float _runSpeed = 60;
		private float _jumpSpeed = 180;
		private float _gravity = 500;

		private Vector2 UpdateInput(Vector2 velocity) {
			Vector2 currentVelocity = velocity;
			currentVelocity.X = 0;
			
			bool right = Input.IsActionPressed("MoveRight");
			bool left = Input.IsActionPressed("MoveLeft");
			bool up = Input.IsActionPressed("MoveUp");
			bool down = Input.IsActionPressed("MoveDown");
			bool jump = Input.IsActionPressed("Jump");

			if (right) {
				currentVelocity.X += _runSpeed;
			}
			if (left) {
				currentVelocity.X -= _runSpeed;
			}
			if (jump && IsOnFloor()) {
				currentVelocity.Y -= _jumpSpeed;
			}
			return currentVelocity;
		}

		private void UpdateSprites() {
			_sprite.FlipH = Velocity.X switch {
				> 0 => false,
				< 0 => true,
				_ => _sprite.FlipH
			};
			if (IsOnFloor()) {
				if (Velocity.X != 0) {
					_sprite.Play("walk");
				} else {
					_sprite.Stop();
				}
			} else {
				if (Velocity.Y < 0) {
					_sprite.Play("up");
				} else if (Velocity.Y > 0) {
					_sprite.Play("down");
				}
			}
		}
		
        public override void _Ready() {
	        // Ascend.LoggerMain.Info("Initializing player scripts...");
            sprite = GetNode<AnimatedSprite2D>("Sprite");
        }

        public override void _PhysicsProcess(double delta) {
	        Vector2 currentVelocity = Velocity;
	        // update gravity
	        currentVelocity.Y += _gravity * (float) delta;
	        // update inputs and movement
	        currentVelocity = UpdateInput(currentVelocity);
	        // return value
			Velocity = currentVelocity;
			MoveAndSlide();
	        UpdateSprites();
        }
    }
}
