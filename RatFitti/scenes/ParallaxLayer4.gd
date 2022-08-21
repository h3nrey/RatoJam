extends ParallaxLayer


export(float) var layer_speed = -1.5

func _process(delta) -> void:
		self.motion_offset.x += layer_speed
