extends KinematicBody2D

onready var raycasts = $raycasts
onready var animate = $anim

export var is_grounded = true

var motion = Vector2.ZERO
var x_input

func _physics_process(delta):
	$Jetpack_particles_left/Particles2D.emitting = false
	$Jetpack_particles_right/Particles2D.emitting = false
	_get_input()
	_handle_facing()
	if x_input != 0:
		_apply_movement(delta)
		
	_apply_gravity(delta)
	_handle_friction(delta)			
	
	is_grounded = _check_is_ground()
	
	if Input.is_action_just_pressed("ui_up"):
		_jump()
	
	if is_grounded:
		PlayerVariables.coyote_time = true
	
	if Input.is_action_pressed("jetpack_up") && PlayerVariables.jetpack_fuel > 0:
		_jetpack()
	
	_set_animation()
		
	if !is_grounded:
		if Input.is_action_just_released("ui_up") and motion.y < -(PlayerVariables.JUMP_FORCE/2):
			motion.y = -(PlayerVariables.JUMP_FORCE/2)
		_reset_coyote_time()
#		$CoyoteTimer.start()
			
	
	motion = move_and_slide(motion, Vector2.UP)
	

func _get_input():
	x_input = int(Input.is_action_pressed("move_right")) - int(Input.is_action_pressed("move_left"))

func _apply_movement(delta):
	motion.x += x_input * PlayerVariables.acceleration * delta * PlayerVariables.TARGET_FPS
	motion.x = clamp(motion.x, -PlayerVariables.max_speed, PlayerVariables.max_speed)
	
	if x_input == 0:	
		if motion.x > 0:
			motion.x -= PlayerVariables.decceleration
		elif motion.x < 0:
			motion.x += min(PlayerVariables.decceleration,0)
	
func _apply_gravity(delta):
	motion.y += PlayerVariables.GRAVITY * delta * PlayerVariables.TARGET_FPS

func _handle_friction(delta):
	if abs(x_input) < 0.01:
		if is_grounded: #friction
			motion.x = lerp(motion.x, 0, PlayerVariables.FRICTION * delta)
		elif !is_grounded: #air resistance
			motion.x = lerp(motion.x, 0, PlayerVariables.AIR_RESISTANCE * delta)

#===== JUMP =====
func  _jump():
	if is_grounded or PlayerVariables.coyote_time:
		motion.y = -(PlayerVariables.JUMP_FORCE)

func _reset_coyote_time():
	yield(get_tree().create_timer(PlayerVariables.coyote_timer), "timeout")
	PlayerVariables.coyote_time = false
	
#===== JETPACK =====
func _jetpack():
	if !is_grounded:
		motion.y -= PlayerVariables.JETPACK_SPEED
		PlayerVariables.jetpack_fuel -= 0.16
		_handle_particles_facing()
		
func _check_is_ground():
	for raycast in raycasts.get_children():
		if raycast.is_colliding():
			return true
		
	return false

func _set_animation():
	var anima = "Idle"
	
	if !is_grounded && motion.y < 0:
		anima = "Jump"
	elif !is_grounded || Input.is_action_just_released("jetpack_up"):
		anima = "Fall"
		
	if x_input != 0 && is_grounded:
		anima = "Walking"
	
	if animate.assigned_animation != anima:
		animate.play(anima)

func _handle_facing():
	if abs(x_input) > 0.01:
		$texture.scale.x = x_input * 0.065
		
func _handle_particles_facing():
	if x_input < 0:
		$Jetpack_particles_right/Particles2D.emitting = true
		$Jetpack_particles_left/Particles2D.emitting = false
		
	elif x_input > 0:
		$Jetpack_particles_left/Particles2D.emitting = true
		$Jetpack_particles_right/Particles2D.emitting = false

