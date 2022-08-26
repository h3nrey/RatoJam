using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private Collider2D currentOneWayPlataform;
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject otherObj = other.gameObject;
        string otherTag = otherObj.tag;

        switch (otherTag) {
            case "Spray":
                PlayerBehaviour.Player._sprayController.rechargeFuel();
                AudioController.audioInstance.Play("colect");
                Destroy(otherObj);
                break;
            case "portal":
                AudioController.audioInstance.Play("sucess");
                SceneCaller._sceneCaller.CallNextScene();
                break;
            case "cheese":
                PlayerBehaviour.Player.cheeseCount += 1;
                AudioController.audioInstance.Play("colect");
                UIController.Ui.UpdateCheeseText(PlayerBehaviour.Player.cheeseCount);
                Destroy(otherObj);
                break;
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameObject otherObj = other.gameObject;
        string otherTag = otherObj.tag;

        switch (otherTag) {
            case "damage":
                //PlayerBehaviour.Player.ResetPlayer();
                AudioController.audioInstance.Play("hurt");
                SceneCaller._sceneCaller.CallScene(SceneCaller._sceneCaller.currentScene);
                print("damaged");
                break;
            case "OneWayPlataform":
                currentOneWayPlataform = otherObj.GetComponent<Collider2D>();
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        GameObject otherObj = other.gameObject;
        string otherTag = otherObj.tag;

        switch (otherTag) {
            case "OneWayPlataform":
                //
                break;
        }
    }

    private IEnumerator DisableCollision() {
        Physics2D.IgnoreCollision(PlayerBehaviour.Player.playerCollider,currentOneWayPlataform);
        yield return new WaitForSeconds(0.25f);
        Physics2D.IgnoreCollision(PlayerBehaviour.Player.playerCollider, currentOneWayPlataform, false);
        currentOneWayPlataform = null;
        print("collision enabled");
    }

    public void CallDisableCollision() {
        StartCoroutine(DisableCollision());
    }
}
