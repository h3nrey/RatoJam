using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatterController : MonoBehaviour
{
    private ParticleSystem particles => PlayerBehaviour.Player.particles;

    [Header("Splatter Effect")]
    [SerializeField] private GameObject splatPrefab;
    [SerializeField] Transform splatHolder;
    [SerializeField] Color[] colors;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void OnParticleCollision(GameObject other) {
        print("Particula colidiu");
        ParticlePhysicsExtensions.GetCollisionEvents(particles, other, collisionEvents);

        int count = collisionEvents.Count;
        print(count);

        if(count <= 10) {
            for (int i = 0; i < count; i++) {
                int randomColorIndex = Random.Range(0, colors.Length);
                GameObject splat = Instantiate(splatPrefab, collisionEvents[i].intersection, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)), splatHolder);
                if(splat.GetComponent<SpriteRenderer>().color == Color.white)
                    splat.GetComponent<SpriteRenderer>().color = colors[randomColorIndex];
            }

        }
    }
}
