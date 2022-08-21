extends Area2D

func _on_Lata_body_entered(body: Node):
	print(body.name)
	if body.name == "Player" && PlayerVariables.jetpack_fuel < 100:
		PlayerVariables.jetpack_fuel = 48
		queue_free()
