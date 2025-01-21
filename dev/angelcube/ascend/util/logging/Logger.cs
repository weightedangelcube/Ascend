using Godot;
using Godot.Collections;

namespace dev.angelcube.ascend.util.logging {
	public class Logger {
		private static string GetStringTime() {
			Dictionary<string, int> dictionaryTime = new(Time.GetTimeDictFromSystem(false));
			string stringTime = "";
			foreach (int v in dictionaryTime.Values) {
				string x = v.ToString().Length > 1 ? v.ToString() : "0" + v.ToString();
				stringTime = stringTime.Equals("") ? string.Concat(stringTime, x) : string.Concat(stringTime, ":", x);
			}
			return stringTime;
		}
		public static void Info(string who, string what) {
			GD.Print(string.Format("[{0}] [{1}/INFO]: {2}", GetStringTime(), who, what));
		}
		public static void Warn(string who, string what) {
			GD.PushWarning(string.Format("[{0}] [{1}/INFO]: {2}", GetStringTime(), who, what));
		}
		//TODO: convert to params for easy stack dumps
		public static void Error(string who, string what) {
			GD.PushError(string.Format("[{0}] [{1}/INFO]: {2}", GetStringTime(), who, what));
		}
	}
}