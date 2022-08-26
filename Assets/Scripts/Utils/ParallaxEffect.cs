using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {
    public GameObject cam;
    public float parallaxEffect;
    public bool autoScroll = false;

    private float length, startpos;

    // Start is called before the first frame update
    void Start() {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update() {


        transform.position -= new Vector3(parallaxEffect * Time.deltaTime, transform.position.y);

        if (transform.position.x < startpos - length) {
            transform.position = new Vector2(startpos, transform.position.y);
        }

    }
}
