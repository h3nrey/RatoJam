extends Area2D



func _on_Espinhos_body_entered(body):
	print(body.name)
	get_tree().reload_current_scene()
