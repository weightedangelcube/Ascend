using Godot;
using Vector2 = Godot.Vector2;

namespace dev.angelcube.ascend.entity.player {
	public partial class Player : CharacterBody2D {
		private AnimatedSprite2D sprite;

		private float runSpeed = 60;
		private float jumpSpeed = 180;
		private float gravity = 500;

		private Vector2 updateInput(Vector2 velocity) {
			Vector2 currentVelocity = velocity;
			currentVelocity.X = 0;
			
			bool right = Input.IsActionPressed("MoveRight");
			bool left = Input.IsActionPressed("MoveLeft");
			bool up = Input.IsActionPressed("MoveUp");
			bool down = Input.IsActionPressed("MoveDown");
			bool jump = Input.IsActionPressed("Jump");

			if (right) {
				currentVelocity.X += runSpeed;
			}
			if (left) {
				currentVelocity.X -= runSpeed;
			}
			if (jump && IsOnFloor()) {
				currentVelocity.Y -= jumpSpeed;
			}
			return currentVelocity;
		}

		private void updateSprites() {
			sprite.FlipH = Velocity.X switch {
				> 0 => false,
				< 0 => true,
				_ => sprite.FlipH
			};
			if (IsOnFloor()) {
				if (Velocity.X != 0) {
					sprite.Play("walk");
				} else {
					sprite.Stop();
				}
			} else {
				if (Velocity.Y < 0) {
					sprite.Play("up");
				} else if (Velocity.Y > 0) {
					sprite.Play("down");
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
	        currentVelocity.Y += gravity * (float) delta;
	        // update inputs and movement
	        currentVelocity = updateInput(currentVelocity);
	        // return value
			Velocity = currentVelocity;
			MoveAndSlide();
	        updateSprites();
        }
    }
}
