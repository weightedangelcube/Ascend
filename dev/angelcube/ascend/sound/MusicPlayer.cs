using dev.angelcube.ascend.util;
using dev.angelcube.ascend.util.logging;
using Godot;
using Godot.Collections;

namespace dev.angelcube.ascend.src.scripts {
	public partial class MusicPlayer : AudioStreamPlayer {
		string Path;
		double BPM;
		double PositionSeconds;
		double PositionBeats;
		double SecsPerBeat;

		public override void _Ready() {
			Dictionary<string, Dictionary> Levels = new(JsonHelper.LoadJson("res://data/levels.json"));
			//TODO: change to use GetCurrentLevel
			Path = (string) Levels["golden"]["music"];
			
			BPM = GD.Load<AudioStreamMP3>(Path)._GetBpm();
			SecsPerBeat = 60 / BPM;

			// Play();
			Logger.Info("main", "Music player successfully loaded");
		}

		public override void _Process(double delta) {
			if (Playing) {
				PositionSeconds = GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
				PositionSeconds -= AudioServer.GetOutputLatency();
				PositionBeats = (PositionSeconds / SecsPerBeat) + 1;
			}
		}
	}
}


