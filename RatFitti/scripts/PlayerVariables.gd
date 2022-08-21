extends Node

const TARGET_FPS = 60

#===== MOVEMENT =====
const acceleration = 9
export var decceleration = 5
export var max_speed = 150

#Friction
const FRICTION = 5
const AIR_RESISTANCE = 1

#===== GRAVITY =====
const GRAVITY = 8

#===== JUMP =====
const JUMP_FORCE = 180
var coyote_time = false
var coyote_timer = 0.2

#===== MOVEMENT =====
const JETPACK_SPEED = 20
export var jetpack_fuel = 48
