extends Node2D

onready var pause = $Pause

onready var cronometro = $Cronometro
var segundos = 00
var minuto = 00
var hora = 00

func _update_time():
	if segundos+1 == 60:
		if minuto+1 == 60:
			hora +=1
			minuto = 0
		minuto += 1
		segundos = 0
	segundos += 1
	
	cronometro = "Tempo: " + str(hora) + ":" + str(minuto) + ":" + str(segundos)

func _on_TextureButton_pressed():
	pause._handle_pause()


func _on_Win_body_entered(body):
	get_tree().change_scene("res://scenes/level_2.tscn")
