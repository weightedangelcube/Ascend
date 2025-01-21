using System;
using System.Reflection.PortableExecutable;
using dev.angelcube.ascend.util.connections.discord;
using dev.angelcube.ascend.util.logging;
using Discord;
using Godot;

namespace dev.angelcube.ascend {
	public partial class Ascend : Node {
        public static readonly long DISCORD_CLIENT_ID = 1331326288383967375;
        DiscordController discordController;
        public override void _Ready() {
            Logger.Info("main", "Loading ascend 0.1.0+alpha.1, enjoy!");
            try {
                discordController = new DiscordController(new Discord.Discord(DISCORD_CLIENT_ID, (ulong) CreateFlags.Default));
                discordController.Init();
            } catch (DllNotFoundException e) {
                Logger.Error("discord", string.Format("Discord GameSDK could not be found: {0}", e.ToString()));
            }
        }

        public override void _Process(double delta) {
            discordController.Update();
        }
    }
}
