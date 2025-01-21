using Godot;
using Godot.Collections;

namespace dev.angelcube.ascend.util {
	public class JsonHelper {
		// RETURNS NULL IF NOT FOUND!!
		public static Dictionary LoadJson(string path) {
			using FileAccess LoadedFile = FileAccess.Open(path, FileAccess.ModeFlags.Read);
			var ParsedJson = Json.ParseString(LoadedFile.GetAsText());
			return (Dictionary) ParsedJson;
        }
	}
}