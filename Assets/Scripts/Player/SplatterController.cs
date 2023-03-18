using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterController : MonoBehaviour
{
    GameObject splatPrefab => PlayerBehaviour.Player.splatPrefab;
    Color[] colors => PlayerBehaviour.Player.colors;
    private int splatLayer;

    private bool canPlaceSplat;

    private void Update() {
        if(PlayerBehaviour.Player.canUseSpray) {
            CreateSplat();
        }
    }
    public void CreateSplat() {
        Vector3 randomRot = new Vector3(0f,0f,Random.Range(0f, 360f));
        GameObject splat = Instantiate(splatPrefab, transform.position, Quaternion.Euler(randomRot));
        splat.GetComponent<SpriteRenderer>().color = colors[Random.Range(0, colors.Length)];

        splatLayer++;
        splat.GetComponent<SpriteRenderer>().sortingOrder = splatLayer;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "ground") {
            canPlaceSplat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "ground") {
            canPlaceSplat = false;
        }
    }
}
