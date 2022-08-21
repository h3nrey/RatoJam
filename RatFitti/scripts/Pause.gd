extends Node
var muted = false

func _ready():
	$Panel.visible = false


func _process(delta):
	if Input.is_action_just_pressed("pause"):
		_handle_pause()
		
func _handle_pause():
	$Panel.visible = !$Panel.visible
	get_tree().paused = !get_tree().paused

#==== BUTTONS ====
func _on_Button_button_down():
	_handle_pause()
	
func _on_Sound_button_down():
	_change_volume()

#==== AUDIO ====
func _change_volume():
	AudioServer.set_bus_volume_db(AudioServer.get_bus_index("Master"), 0)
	muted = !muted
	print("entrou no audio")

