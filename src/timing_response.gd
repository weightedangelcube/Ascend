extends Node

@export var song_bpm = 60

var timing_window = (song_bpm / 60.0) / 5
var user_input_latency = 0.0

var press_time = 0.0

var beat_time = 0.0

signal on_time()
var is_on_time = false
var timing = 0.0

func _on_beat_report(beat):
	beat_time = beat
# 	if Input.is_action_just_released("chirp"):
# 		press_time = get_parent().beats_position - user_input_latency
# 		var is_on_time = abs(press_time - beat_time) <= timing_window
# 		if is_on_time:
# 			print("hit!")
# 		else:
# 			print("miss!")
# 	else:
# 		print("miss!")

func _process(_delta):
	if Input.is_action_just_released("chirp"):
		press_time = get_parent().beats_position - user_input_latency
		timing  = press_time - beat_time
		is_on_time = (press_time - beat_time) >= (1 - timing_window) || (press_time - beat_time) <= timing_window
		# is_on_time = abs(press_time - beat_time) <= timing_window
		if is_on_time:
			print("hit!")
			on_time.emit()
		else:
			print("miss!")
