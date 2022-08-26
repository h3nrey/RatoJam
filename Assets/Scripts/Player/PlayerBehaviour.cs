using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    public static PlayerBehaviour Player;
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float decceleration;
    [SerializeField] float velPower;
    public Vector2 vel;

    float moveInput;

    [Header("Friction")]
    [SerializeField] private float frictionAmount;
    [SerializeField] private float airResistance;

    [Header("Points")]
    [SerializeField] Transform groundPoint;
    [SerializeField] float groundRadius;
    [SerializeField] LayerMask groundLayer;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCutMultiplier;
    [SerializeField] float coyoteTimeCounter;
    [SerializeField] float coyoteTime = 0.2f;

    [Header("Gravity")]
    [SerializeField] float gravityScale;
    [SerializeField] float fallGravityMultiplier;

    [Header("Spray")]
    [SerializeField] public float sprayFuel;
    [SerializeField] public float fuelRechargeAmount;
    [SerializeField] public float totalSprayFuel;
    [SerializeField] public float sprayForce;
    [SerializeField] public float sprayFallingForce;
    [SerializeField] public float sprayAmountUsed;
    [SerializeField] public bool canUseSpray;

    [Header("Colectables")]
    [SerializeField] public int cheeseCount;

    [Header("States")]
    public bool isGrounded = true;

    [Header("Reset")]
    [SerializeField] Transform checkPoint;

    //Components
    public Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] public ParticleSystem particles;
    [SerializeField] public Collider2D playerCollider;

    //Other Scripts
    public SprayController _sprayController;
    public CollisionController _collisionController;

    private void Awake() {
        Player = this;
    }

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        HandleCoyoteJump();
        SetAnimation();
        CheckFacing();
    }

    void FixedUpdate() {
        vel = rb.velocity;
        float targetSpeed = moveInput * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);

        handleGravity();
        checkGround();
        Friction();
    }

    void checkGround() {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, groundRadius, groundLayer);
    }

    void Friction() {
        if(isGrounded && Mathf.Abs(moveInput) < 0.01f) {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(Mathf.Sign(rb.velocity.x));
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        } else if (!isGrounded) {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(airResistance));
            amount *= Mathf.Sign(Mathf.Sign(rb.velocity.x));
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    void handleGravity() {
        if(vel.y < 0) {
            rb.gravityScale = gravityScale * fallGravityMultiplier;
        } else {
            rb.gravityScale = gravityScale;
        }
    }

    public void Jump(InputAction.CallbackContext context) {
        if(context.started && isGrounded || coyoteTimeCounter > 0) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            coyoteTimeCounter = 0;
        }

        if(context.canceled && vel.y > 0) {
            rb.velocity = new Vector2(vel.x, vel.y * jumpCutMultiplier);
            coyoteTimeCounter = 0;
        }
    }

    private void ExecuteJump() {
        print("Jump executed");
        if(isGrounded) {
            if (vel.y <= 0) {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (vel.y > 0) {
                rb.velocity = new Vector2(vel.x, vel.y * jumpCutMultiplier);
            }
            coyoteTimeCounter = 0f;
        }
        
    }

    private void HandleCoyoteJump() {
        if(isGrounded) {
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void Spray(InputAction.CallbackContext context) {
        if(context.started) {
            AudioController.audioInstance.Play("spray");
            canUseSpray = true;
            print("sprayed");
        }

        if(context.canceled) {
            canUseSpray = false;
        }
    }

    public void GoDown(InputAction.CallbackContext context) {
        if (context.started) {
            print("chamando go down");
            _collisionController.CallDisableCollision();
        }
    }

    public void GetInput(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>().x;
    }

    private void SetAnimation() {
        anim.SetFloat("speed", Mathf.Abs(moveInput));
        anim.SetFloat("yVelocity", vel.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void CheckFacing() {
        if(moveInput > 0.01) {
            transform.rotation = Quaternion.Euler(Vector3.up * 0);
        } else if (moveInput < -0.01) {
            transform.rotation = Quaternion.Euler(Vector3.up * 180);
        }
    }

    public void ResetPlayer() {
        transform.position = checkPoint.position;
        rb.velocity = Vector2.zero;
        sprayFuel = totalSprayFuel;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(groundPoint.position, groundRadius);
    }
}
