using System;
using Godot;

namespace dev.angelcube.ascend.util.logging {
	public class Logger {
		private readonly FileAccess file;
		private readonly string name;
		private const string LOGGER_PATH = "user://logs/latest.log";
		private static readonly string DATE_TIME_FORMAT = DateTime.Now.ToString("MM-dd-yyyy_HH.mm.ss");
		private static readonly string TIME_FORMAT = DateTime.Now.ToString("HH:mm:ss");
		public Logger(string name) {
			this.name = name;
			DirAccess.MakeDirAbsolute("user://logs/");
			if (FileAccess.FileExists(LOGGER_PATH)) {
				// if latest log already exists, retrieve it and rename it
				byte[] existing = FileAccess.GetFileAsBytes(LOGGER_PATH);
				FileAccess newFile = FileAccess.Open($"user://logs/{DATE_TIME_FORMAT}.log", FileAccess.ModeFlags.Write);
				newFile.StoreBuffer(existing);
				newFile.Close();
				DirAccess.RemoveAbsolute(LOGGER_PATH);
			}
			file = FileAccess.Open(LOGGER_PATH, FileAccess.ModeFlags.Write);
		}

		private void write(Entry entry) {
			GD.Print(entry.ToString());
			file.StoreLine(entry.ToString());
			file.Flush();
		}

		public void deinit() {
			file.Close();
		}
		
		public void trace(string message) {
			write(new Entry(name, message, Entry.Level.trace));
		}
		
		public void debug(string message) {
			write(new Entry(name, message, Entry.Level.debug));
		}

		public void info(string message) {
			write(new Entry(name, message, Entry.Level.info));
		}
		
		public void warn(string message) {
			write(new Entry(name, message, Entry.Level.warn));
		}
		
		public void error(string message) {
			write(new Entry(name, message, Entry.Level.error));
		}
		
		public sealed record Entry {
			private readonly string name;
			private readonly string message;
			private readonly Level level;
			private readonly DateTime timestamp;
			public Entry(string name, string message, Level level) {
				this.name = name;
				this.message = message;
				this.level = level;
				this.timestamp = DateTime.Now;
			}

			public override string ToString() {
				return $"[{timestamp.ToString(TIME_FORMAT)}] [{name}/{level.ToString().ToUpper()}]: {message}";
			}
			
			public enum Level {
				trace = 0,
				debug = 1,
				info = 2,
				warn = 3,
				error = 4
			}
		}
	}
}