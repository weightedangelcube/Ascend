using dev.angelcube.ascend.level;
using dev.angelcube.ascend.util.connections.discord;
using dev.angelcube.ascend.util.logging;
using DiscordRPC;
using Godot;

namespace dev.angelcube.ascend {
	public partial class Ascend : Node {
        private const string DISCORD_CLIENT_ID = "1331326288383967375";
        private DiscordController discordController;
        private readonly Logger loggerMain = new("main");
        
        private CharacterBody2D player;
        private Camera2D camera;
        private Level level;
        public override void _Ready() {
            foreach (Node child in GetChildren()) {
                if (child.Name == "Player") {
                    player = child as CharacterBody2D;
                }
                if (child.Name == "Camera") {
                    camera = child as Camera2D;
                }
                if (child.Name == "Level") {
                    level = child as Level;
                }
            }
            loggerMain.info("Initializing ascend, enjoy!");
            discordController = new DiscordController(new DiscordRpcClient(DISCORD_CLIENT_ID));

            if (OS.IsDebugBuild()) {
                discordController.initDebug();
            } else {
                discordController.init();
            }
        }

        public override void _Process(double delta) {
            Vector2 cameraPos = camera.GlobalPosition;
            cameraPos.X = player.GlobalPosition.X + 60;
            camera.GlobalPosition = cameraPos;
        }
        
        public override void _Notification(int notificationId) {
            if (notificationId == NotificationWMCloseRequest) {
                loggerMain.deinit();
                discordController.deinit();
                GetTree().Quit();
            }
        }
    }
}
