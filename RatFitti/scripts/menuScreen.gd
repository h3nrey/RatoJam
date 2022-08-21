extends Control

func _ready():
	pass 
	
func _on_HelpButton_pressed() -> void:
	get_tree().change_scene("res://scenes/Rules.tscn")

func _on_SoundButton_button_down():
	var music_bus = AudioServer.get_bus_index("Master")
	AudioServer.set_bus_mute(music_bus, not AudioServer.is_bus_mute(music_bus))
