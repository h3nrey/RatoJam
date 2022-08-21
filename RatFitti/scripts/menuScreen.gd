extends Control

func _ready():
	pass 
	
func _on_HelpButton_pressed() -> void:
	get_tree().change_scene("res://scenes/Rules.tscn")
