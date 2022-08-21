extends Control

onready var fuel_bar = $fuelBar
onready var bar_states= [load("res://UI/Barra100_.png"),load("res://UI/Barra80_.png"),load("res://UI/Barra60_.png"),load("res://UI/Barra40_.png"),load("res://UI/Barra20_.png"),load("res://UI/Barra0_.png")]
# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	print(PlayerVariables.jetpack_fuel)
	if PlayerVariables.jetpack_fuel >= 40:
		fuel_bar.set_texture(bar_states[0])
	elif PlayerVariables.jetpack_fuel >= 35:
		fuel_bar.set_texture(bar_states[1])
	elif PlayerVariables.jetpack_fuel >= 25:
		fuel_bar.set_texture(bar_states[2])
	elif PlayerVariables.jetpack_fuel >= 15:
		fuel_bar.set_texture(bar_states[3])
	elif PlayerVariables.jetpack_fuel >= 5:
		fuel_bar.set_texture(bar_states[4])
	elif PlayerVariables.jetpack_fuel >= 0:
		fuel_bar.set_texture(bar_states[5])
