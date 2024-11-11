extends AudioStreamPlayer

# Song BPM
#TODO: you CAN'T just set the value in the script, make sure to have a JSON handy later
@export var song_bpm := 72.0

signal beat_report(_beat)

# Counting the song position in seconds and beats
var secs_position = 0.0
var beats_position = 0.0

var secs_per_beat

var last_beat = 0
var beat = 0
var measure = 0

#func _input(event: InputEvent) -> void:

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	secs_per_beat = 60.0 / song_bpm
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
		
		_report_beat()
		
func _report_beat() -> void:
	if last_beat < floori(beats_position):
		last_beat = floori(beats_position)
		if (last_beat - 1) % 4 == 0:
			measure += 1
		print("absbeat %d, measure %d" % [last_beat, measure])
		beat_report.emit(last_beat)
