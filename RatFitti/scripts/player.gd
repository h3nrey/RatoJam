extends KinematicBody2D

var velocity = Vector2.ZERO
var speed = 280
var gravity = 1200
var jump_force = -750
var is_grounded
onready var raycasts = $raycasts
onready var animate = $anim

func _physics_process(delta: float) -> void:
	velocity.y += gravity * delta
	
	_get_input()
	_set_animation()
	move_and_slide(velocity)
	
	is_grounded = _check_is_ground()

func _get_input():
	var move_direction = int(Input.is_action_pressed("move_right")) - int(Input.is_action_pressed("move_left"))
	velocity.x = lerp(velocity.x, (move_direction * speed), 0.2)
	
	if move_direction != 0:
		$texture.scale.x = move_direction

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("jump") && is_grounded:
			velocity.y = jump_force / 2
	
func _check_is_ground():
	for raycast in raycasts.get_children():
		if raycast.is_colliding():
			return true
		
	return false

func _set_animation():
	var anima = "idle"
	
	if !is_grounded:
		anima = "jump"
	elif velocity.x != 0:
		anima = "running"
	
	if animate.assigned_animation != anima:
		animate.play(anima)
