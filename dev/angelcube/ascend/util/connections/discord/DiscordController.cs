using System;
using dev.angelcube.ascend.util.logging;
using Discord;

namespace dev.angelcube.ascend.util.connections.discord {
	public class DiscordController {
		public Discord.Discord discord;

		public DiscordController(Discord.Discord discord) {
			this.discord = discord;
		}
		// This still only works for x86_64 CPUs.
		// I haven't implemented it for other CPUs yet.
		public void Init() {
            ActivityManager activityManager = discord.GetActivityManager();
            Activity activity = new() {
                State = "gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming",
                Details = "Debug Mode",
				Assets = {
					LargeImage = "placeholder",
					LargeText = "This is a debug placeholder"
				}
            };
            activityManager.UpdateActivity(activity, (callback) => {
                if (callback == Result.Ok) {
                    Logger.Info("discord", "Activity updated successfully");
                } else {
                    Logger.Error("discord", string.Format("Updating activity threw an error: {0}", callback.ToString("D")));
                }
            });
		}

		public void Update() {
			discord?.RunCallbacks();
		}
	}
}