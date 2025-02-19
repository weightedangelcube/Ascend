using Godot;
using Godot.Collections;

namespace dev.angelcube.ascend.util {
	public class JsonHelper {
		// RETURNS NULL IF NOT FOUND!!
		public static Dictionary LoadJson(string path) {
			using FileAccess loadedFile = FileAccess.Open(path, FileAccess.ModeFlags.Read);
			Variant parsedJson = Json.ParseString(loadedFile.GetAsText());
			return (Dictionary) parsedJson;
        }
	}
}