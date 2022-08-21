extends Node
var muted = false
var paused

func _ready():
	$Panel.visible = false
		
func _handle_pause():
	_handle_pause_song()
		
	$Panel.visible = !$Panel.visible
	get_tree().paused = !get_tree().paused

func _handle_pause_song():
	if !paused:
		$Panel/Song.play()
	else:
		$Panel/Song.stop()
	


#==== BUTTONS ====
func _on_ContinueButton_pressed():
	_handle_pause()
	
func _on_SoundButton_pressed():
	_change_volume()
	


#==== AUDIO ====
func _change_volume():
	var music_bus = AudioServer.get_bus_index("Master")
	AudioServer.set_bus_mute(music_bus, not AudioServer.is_bus_mute(music_bus))
