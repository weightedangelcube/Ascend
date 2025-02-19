using DiscordRPC;

namespace dev.angelcube.ascend.util.connections.discord {
	public class DiscordController {
		private readonly DiscordRpcClient client;

		public DiscordController(DiscordRpcClient client) {
			this.client = client;
		}
		// FIX: might work only for x86
		public void init() {
			client.Initialize();
			client.SetPresence(
				new RichPresence() {
					State = "Gaming!",
				});
		}
		
		public void initDebug() {
			client.Initialize();
			client.SetPresence(
				new RichPresence() {
					State = "Debugging",
					Assets = new Assets() {
						LargeImageKey = "placeholder",
						LargeImageText = "This is a debug placeholder"
					}
				});
		}

		public void deinit() {
			client.Dispose();
		}
	}
}