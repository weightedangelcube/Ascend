using DiscordRPC;

namespace dev.angelcube.ascend.util.connections.discord {
	public class DiscordController {
		private readonly DiscordRpcClient _client;

		public DiscordController(DiscordRpcClient client) {
			this._client = client;
		}
		// FIX: might work only for x86
		public void Init() {
			_client.Initialize();
			_client.SetPresence(
				new RichPresence() {
					State = "Playing gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming gaming",
					Details = "In debug mode",
					Assets = new Assets() {
						LargeImageKey = "placeholder",
						LargeImageText = "This is a debug placeholder"
					}
				});
		}

		public void Deinit() {
			_client.Dispose();
		}
	}
}