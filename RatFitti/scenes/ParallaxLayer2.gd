extends ParallaxLayer


export(float) var layer_speed = -0.25

func _process(delta) -> void:
		self.motion_offset.x += layer_speed
