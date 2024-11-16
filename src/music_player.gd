extends AudioStreamPlayer

# Song BPM
#TODO: you CAN'T just set the value in the script, make sure to have a JSON handy later
@export var song_bpm = 60

signal beat_report(beat)

# Counting the song position in seconds and beats
var secs_position = 0.0
var beats_position = 0.0

var secs_per_beat = 0.0
var timing_window = 0.0

var last_beat = 0
var beat = 0
var measure = 0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	secs_per_beat = 60.0 / song_bpm
	timing_window = (song_bpm / 60.0) / 5
	play()

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if playing:
		# Get playback position, with chunk latency
		secs_position = get_playback_position() + AudioServer.get_time_since_last_mix()
		# Then remove output latency
		secs_position -= AudioServer.get_output_latency()
		
		# Calculate the number of beats
		beats_position = (secs_position / secs_per_beat) + 1
		
		# _report_window_start()
		_report_beat()

# var temp = 0.0

# func _report_window_start() -> void:
# 	temp = (last_beat - 1) + (1 - timing_window)
# 	if (temp >= beats_position) || (beats_position < temp):
# 		print("window start!")

func _report_beat() -> void:
	if last_beat < floori(beats_position):
		last_beat = floori(beats_position)
		if (last_beat - 1) % 4 == 0:
			measure += 1
		# print("beat!")
		beat_report.emit(last_beat)
