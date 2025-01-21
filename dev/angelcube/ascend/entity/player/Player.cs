using dev.angelcube.ascend.util.logging;
using Godot;
using Vector2 = Godot.Vector2;

namespace dev.angelcube.ascend.entity.player {
	public partial class Player : Area2D {
		[Export]
		public int Speed { get; set; } = 50;
		public Vector2 ScreenSize;

        public override void _Ready() {
			Logger.Info("main", "Player scripts successfully loaded");
            ScreenSize = GetViewportRect().Size;
        }

        public override void _Process(double delta) {
            Vector2 velocity = Vector2.Zero;

			if (Input.IsActionPressed("MoveRight")) {
				velocity.X += 1;
			}
			
			if (Input.IsActionPressed("MoveLeft")) {
				velocity.X -= 1;
			}

			if (Input.IsActionPressed("MoveDown")) {

			}

			if (Input.IsActionPressed("MoveUp")) {
				
			}

			// remove bhop if desired
			if (velocity.Length() > 0) {
		        velocity = velocity.Normalized() * Speed;
			}

			Position += velocity * (float) delta;
			Position = new Vector2(
				Mathf.Clamp(Position.X, 0, ScreenSize.X),
				Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
			);

		}
    }
}
