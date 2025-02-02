using System;
using dev.angelcube.ascend.level;
using dev.angelcube.ascend.util.connections.discord;
using dev.angelcube.ascend.util.logging;
using DiscordRPC;
using Godot;

namespace dev.angelcube.ascend {
	public partial class Ascend : Node {
        private const string DiscordClientId = "1331326288383967375";
        private DiscordController _discordController;
        
        private CharacterBody2D _player;
        private Camera2D _camera;
        private Level _level;
        public override void _Ready() {
            foreach (Node child in GetChildren()) {
                if (child.Name == "Player" && child is CharacterBody2D cBody2D) {
                    _player = cBody2D;
                }
                if (child.Name == "Camera" && child is Camera2D camera2D) {
                    _camera = camera2D;
                }
                if (child.Name == "Level" && child is Level level) {
                    _level = level;
                }
            }
            Logger.Info("main", "Loading ascend 0.1.0+alpha.1, enjoy!");
            try {
                _discordController = new DiscordController(new DiscordRpcClient(DiscordClientId));
                _discordController.Init();
            } catch (DllNotFoundException e) {
                Logger.Error("discord", $"Discord GameSDK could not be found: {e}");
            }
        }

        public override void _Process(double delta) {
            Vector2 cameraPos = _camera.GlobalPosition;
            cameraPos.X = _player.GlobalPosition.X + 60;
            _camera.GlobalPosition = cameraPos;
        }
        
        public override void _Notification(int notificationId) {
            if (notificationId == NotificationWMCloseRequest) {
                _discordController.Deinit();
                GetTree().Quit();
            }
        }
    }
}
