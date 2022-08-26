using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayController : MonoBehaviour
{
    private bool canUseSpray => PlayerBehaviour.Player.canUseSpray;
    private float sprayForce => PlayerBehaviour.Player.sprayForce;
    private float totalSprayFuel => PlayerBehaviour.Player.totalSprayFuel;
    private float sprayFallingForce => PlayerBehaviour.Player.sprayFallingForce;
    private float sprayAmountUsed => PlayerBehaviour.Player.sprayAmountUsed;
    private float fuelRechargeAmount => PlayerBehaviour.Player.fuelRechargeAmount;

    private ParticleSystem particles => PlayerBehaviour.Player.particles;

    private Vector2 vel => PlayerBehaviour.Player.vel;
    private Rigidbody2D rb => PlayerBehaviour.Player.rb;

    private bool isGrounded {
        get => PlayerBehaviour.Player.isGrounded;
        set => PlayerBehaviour.Player.isGrounded = value;
    }

    private float sprayFuel {
        get => PlayerBehaviour.Player.sprayFuel;
        set => PlayerBehaviour.Player.sprayFuel = value;
    }

    private void Start() {
        sprayFuel = totalSprayFuel;
    }

    private void Update() {
        HandleFuelUsage();
    }

    void FixedUpdate()
    {
        useSpray();
    }
    private void useSpray() {
        if (canUseSpray && sprayFuel > 0) {
            if (vel.y > 0) {
                rb.AddForce(Vector2.up * sprayForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

            }
            else if (vel.y < 0) {
                rb.AddForce(Vector2.up * sprayFallingForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            }
            if (!particles.isEmitting && particles.isPlaying)
                particles.Stop();
            AudioController.audioInstance.Play("spray");
            particles.Play();
            sprayFuel -= sprayAmountUsed;
        } else if(!canUseSpray){
            particles.Stop();
        }
    }

    private void HandleFuelUsage() {
        float sprayPercent = sprayFuel / totalSprayFuel * 100;

        if (sprayPercent >= 99) {
            UIController.Ui.UpdateFuelBar(UIController.Ui.fuelImages[5]);
        }  else if (sprayPercent >= 80) {
            UIController.Ui.UpdateFuelBar(UIController.Ui.fuelImages[4]);
        } else if (sprayPercent >= 60) {
            UIController.Ui.UpdateFuelBar(UIController.Ui.fuelImages[3]);
        } else if (sprayPercent >= 40) {
            UIController.Ui.UpdateFuelBar(UIController.Ui.fuelImages[2]);
        } else if (sprayPercent >= 20) {
            UIController.Ui.UpdateFuelBar(UIController.Ui.fuelImages[1]);
        } else if (sprayPercent >= 0) {
            UIController.Ui.UpdateFuelBar(UIController.Ui.fuelImages[0]);
        }
    }

    public void rechargeFuel() {
        sprayFuel += fuelRechargeAmount;
    }
}
