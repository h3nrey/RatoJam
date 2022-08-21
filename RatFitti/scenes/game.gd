extends Node2D

onready var pause = $Pause

func _ready():
	pass # Replace with function body.



func _on_TextureButton_pressed():
	pause._handle_pause()
